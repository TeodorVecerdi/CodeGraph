using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public class NewNodeWindow {
        private Vector2 position;
        private GraphEditorWindow window;
        private int selected = 0;
        private float width = 200f;
        private float height = 200f;
        private float itemHeight = 20f;

        private string searchText = "";
        private string oldSearchText = "";
        private List<string> filteredNodes = new List<string>(allNodes);
        private Vector2 scroll = Vector2.zero;
        private float viewRectHeight = 20f * allNodes.Count;

        private static List<string> allNodes = new List<string> {
            "Class Node",
            "CodeBranch Node",
            "DebugLog Node",
            "If Node",
            "StartEvent Node",
            "UpdateEvent Node",
            "Vector2 Node",
            "Equals Node",
            "GreaterEqual Node",
            "Greater Node",
            "LessEqual Node",
            "Less Node",
            "NotEquals Node"
        };

        public NewNodeWindow(Vector2 position, GraphEditorWindow window) {
            this.position = position;
            this.window = window;
        }

        public void Render() {
            GUI.SetNextControlName("newNodeWindowSearch");
            searchText = GUI.TextField(new Rect(position, new Vector2(width, itemHeight)), searchText).Trim();
            EditorGUI.FocusTextInControl("newNodeWindowSearch");
            scroll = GUI.BeginScrollView(new Rect(position.x, position.y + itemHeight, width, height), scroll, new Rect(0, 0, width - 20, viewRectHeight));
            var i = 0;
            filteredNodes.ForEach(node => {
                Color color = new Color(0.2f, 0.2f, 0.2f);
                if (selected == i) color = new Color(0.25f, 0.25f, 0.25f);
                DrawQuad(new Rect(0, i * itemHeight, width - 20, itemHeight), color);
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(0, i * itemHeight, width - 20, itemHeight), node);

                // GUI.Button(new Rect(0, i++ * itemHeight, width - 20, itemHeight), node);
                i++;
            });
            GUI.EndScrollView();
        }

        public void Update() {
            if (!oldSearchText.Equals(searchText)) {
                selected = 0;
                filteredNodes = allNodes.Where(node => node.ToLower().Contains(searchText.ToLower())).ToList();
                viewRectHeight = Mathf.Max(height, filteredNodes.Count * itemHeight);
            }

            if (Event.current.type == EventType.KeyDown) {
                var count = filteredNodes.Count;
                if (Event.current.keyCode == KeyCode.DownArrow) {
                    selected = ((selected + 1) % count + count) % count;
                    scroll.y = itemHeight * selected;
                }
                if (Event.current.keyCode == KeyCode.UpArrow) {
                    selected = ((selected - 1) % count + count) % count;
                    scroll.y = itemHeight * selected;
                }
            }

            oldSearchText = searchText;
        }

        private void DrawQuad(Rect position, Color color) {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(position, GUIContent.none);
        }
    }
}