using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider {
        private CodeGraph editorWindow;
        private CodeGraphView graphView;
        private Texture2D icon;
        private List<SearchTreeEntry> searchTree;

        public void Initialize(CodeGraph editorWindow, CodeGraphView graphView) {
            this.editorWindow = editorWindow;
            this.graphView = graphView;

            // Transparent icon to trick search window into indenting items
            icon = new Texture2D(1, 1);
            icon.SetPixel(0, 0, new Color(0, 0, 0, 0));
            icon.Apply();
        }

        private void OnDestroy() {
            if (icon != null) {
                DestroyImmediate(icon);
                icon = null;
            }
        }

        private readonly struct NodeEntry : IEquatable<NodeEntry> {
            public readonly string[] Title;
            public readonly Type NodeType;

            public NodeEntry(Type nodeType, string[] title) {
                NodeType = nodeType;
                Title = title;
            }

            public bool Equals(NodeEntry other) {
                return Equals(Title, other.Title) && NodeType == other.NodeType;
            }

            public override bool Equals(object obj) {
                return obj is NodeEntry other && Equals(other);
            }

            public override int GetHashCode() {
                unchecked {
                    return ((Title != null ? Title.GetHashCode() : 0) * 397) ^ (NodeType != null ? NodeType.GetHashCode() : 0);
                }
            }
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
            return searchTree ?? (searchTree = BuildSearchTree());
        }

        private void PrintEntries(List<NodeEntry> entries) {
            var sb = new StringBuilder();
            sb.AppendLine("Node entries:");
            foreach (var entry in entries) {
                sb.AppendLine($"    {entry.Title.Join("/")} => {entry.NodeType.Name}");
            }
            Debug.Log(sb.ToString());
        }

        private List<SearchTreeEntry> BuildSearchTree() {
            // First build up temporary data structure containing group & title as an array of strings (the last one is the actual title) and associated node type.
            var nodeEntries = new List<NodeEntry>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in GetTypesOrNothing(assembly)) {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(AbstractNode))) {
                        var attrs = type.GetCustomAttributes(typeof(TitleAttribute), false) as TitleAttribute[];
                        if (attrs != null && attrs.Length > 0) {
                            // var nodeAttr = type.GetCustomAttribute<NodeAttribute>();
                            // if (nodeAttr == null || (editorWindow.GraphObject.CodeGraphData.IsMonoBehaviour && !nodeAttr.AllowOnMonoBehaviourGraph) || (!editorWindow.GraphObject.CodeGraphData.IsMonoBehaviour && !nodeAttr.AllowOnClassGraph)) continue;
                            AddEntries(type, attrs[0].title, nodeEntries);
                        }
                    }
                }
            }

            // Sort the entries lexicographically by group then title with the requirement that items always comes before sub-groups in the same group.
            // Example result:
            // - Art/BlendMode
            // - Art/Adjustments/ColorBalance
            // - Art/Adjustments/Contrast
            nodeEntries.Sort((entry1, entry2) => {
                for (var i = 0; i < entry1.Title.Length; i++) {
                    if (i >= entry2.Title.Length)
                        return 1;
                    var value = string.Compare(entry1.Title[i], entry2.Title[i], StringComparison.Ordinal);
                    if (value != 0) {
                        // Make sure that leaves go before nodes
                        if (entry1.Title.Length != entry2.Title.Length && (i == entry1.Title.Length - 1 || i == entry2.Title.Length - 1))
                            return entry1.Title.Length < entry2.Title.Length ? -1 : 1;
                        return value;
                    }
                }

                return 0;
            });

            //* Build up the data structure needed by SearchWindow.

            // `groups` contains the current group path we're in.
            var groups = new List<string>();

            // First item in the tree is the title of the window.
            var tree = new List<SearchTreeEntry> {
                new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
            };

            foreach (var nodeEntry in nodeEntries) {
                // `createIndex` represents from where we should add new group entries from the current entry's group path.
                var createIndex = int.MaxValue;

                // Compare the group path of the current entry to the current group path.
                for (var i = 0; i < nodeEntry.Title.Length - 1; i++) {
                    var group = nodeEntry.Title[i];
                    if (i >= groups.Count) {
                        // The current group path matches a prefix of the current entry's group path, so we add the
                        // rest of the group path from the currrent entry.
                        createIndex = i;
                        break;
                    }

                    if (groups[i] != group) {
                        // A prefix of the current group path matches a prefix of the current entry's group path,
                        // so we remove everyfrom from the point where it doesn't match anymore, and then add the rest
                        // of the group path from the current entry.
                        groups.RemoveRange(i, groups.Count - i);
                        createIndex = i;
                        break;
                    }
                }

                // Create new group entries as needed.
                // If we don't need to modify the group path, `createIndex` will be `int.MaxValue` and thus the loop won't run.
                for (var i = createIndex; i < nodeEntry.Title.Length - 1; i++) {
                    var group = nodeEntry.Title[i];
                    groups.Add(group);
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(group)) {level = i + 1});
                }

                // Finally, add the actual entry.
                tree.Add(new SearchTreeEntry(new GUIContent(nodeEntry.Title.Last(), icon)) {level = nodeEntry.Title.Length, userData = nodeEntry});
            }

            return tree;
        }

        public static IEnumerable<Type> GetTypesOrNothing(Assembly assembly) {
            try {
                return assembly.GetTypes();
            } catch {
                return Enumerable.Empty<Type>();
            }
        }

        void AddEntries(Type nodeType, string[] title, List<NodeEntry> nodeEntries) {
            nodeEntries.Add(new NodeEntry(nodeType, title));
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context) {
            var nodeEntry = (NodeEntry) entry.userData;
            var nodeType = nodeEntry.NodeType;
            var node = (AbstractNode) Activator.CreateInstance(nodeType);
            var windowMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
            var graphMousePosition = graphView.contentViewContainer.WorldToLocal(windowMousePosition);

            var nodePosition = new Vector2(graphMousePosition.x, graphMousePosition.y);
            node.SetPosition(new Rect(nodePosition, AbstractNode.DefaultNodeSize));
            node.OnCreateFromSearchWindow(nodePosition);
            graphView.AddElement(node);
            return true;
        }
    }
}