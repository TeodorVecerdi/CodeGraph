using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class CodeGraph : EditorWindow {
        public static CodeGraph Instance;
        public CodeGraphView GraphView;
        public CodeGraphObject GraphObject;
        private Toolbar toolbar;
        private Button saveButton;

        [MenuItem("Graph/Code Graph")]
        public static void CreateGraphViewWindow() {
            var window = GetWindow<CodeGraph>();
            Instance = window;
            window.titleContent = new GUIContent("CodeGraph Editor", Resources.Load<Texture2D>("codegraph_256"));
        }

        public void SetGraph(CodeGraphObject graphObject) {
            this.GraphObject = graphObject;
        }

        public void Initialize() {
            LoadGraph();
        }

        public void LoadGraph() {
            SaveUtility.GetInstance(GraphView).LoadGraph(GraphObject);
        }

        public void SaveGraph(bool shouldRefreshAssets = true) {
            ValidateSaveButton();
            var newGraphObject = SaveUtility.GetInstance(GraphView).Save(GraphObject.CodeGraphData.AssetPath, shouldRefreshAssets);
            GraphObject = newGraphObject;
        }

        private void ConstructGraphView() {
            Instance = this;
            GraphView = new CodeGraphView(this) {
                name = "Code Graph"
            };
            GraphView.StretchToParentSize();
            rootVisualElement.Add(GraphView);
            if (GraphObject != null) {
                SaveUtility.GetInstance(GraphView).LoadGraph(GraphObject);
            }
            
            GraphView.RegisterCallback<KeyDownEvent>(evt => {
                if (evt.keyCode == KeyCode.S && evt.ctrlKey) {
                    SaveGraph();
                    evt.StopPropagation();
                }
                if (evt.keyCode == KeyCode.G && evt.ctrlKey && !evt.shiftKey && GraphView.selection.Count > 0) {
                    GraphView.GroupSelection();
                    evt.StopPropagation();
                }
                if (evt.keyCode == KeyCode.G && evt.ctrlKey && evt.shiftKey && GraphView.selection.Count > 0) {
                    GraphView.UngroupSelection();
                    evt.StopPropagation();
                }
            }, TrickleDown.TrickleDown);
        }

        private void GenerateToolbar() {
            saveButton = new Button(() => SaveGraph()) {text = "Save Graph", tooltip = "Saves the current CodeGraph file. This does not compile the CodeGraph into C# code."};
            toolbar = new Toolbar();
            toolbar.Add(saveButton);
            toolbar.Add(new Button(() => {
                SaveGraph(false);
                var code = GenerateCode();
                var errors = CompileCode(code);
                if (errors.Count > 0) {
                    errors.ForEach(error => Debug.LogError($"Error {error.ErrorNumber} => \"{error.ErrorText} at line number {error.Line}\"\r\n"));
                    return;
                }

                var assetPath = WriteCodeToFile(code);
                AssetDatabase.ImportAsset(assetPath);
            }) {text = "Compile Graph", tooltip = "Compiles this CodeGraph into a C# file in the same directory as this CodeGraph file"});
            rootVisualElement.Add(toolbar);
        }

        private void ValidateSaveButton() {
            saveButton.text = "Save Graph";
            saveButton.tooltip = "Saves the current CodeGraph file. This does not compile the CodeGraph into C# code.";
            saveButton.RemoveFromClassList("saveButton-unsaved");
        }

        public void InvalidateSaveButton() {
            saveButton.text = "Save Graph (*)";
            saveButton.tooltip = "There are unsaved changes! Saves the current CodeGraph file. This does not compile the CodeGraph into C# code.";
            saveButton.AddToClassList("saveButton-unsaved");
        }

        private string GenerateCode() {
            var monobehaviourName = GraphObject.CodeGraphData.GraphName;
            monobehaviourName = monobehaviourName.Replace(" ", "");
            var code = new StringBuilder();
            code.AppendLine($"using UnityEngine;\npublic class {monobehaviourName} : MonoBehaviour {{");
            var allNodes = GraphView.nodes.ToList();

            // Property Nodes
            foreach (var node in allNodes.OfType<CreatePropertyNode>()) {
                code.AppendLine(node.GetCode());
            }

            // Event Nodes
            foreach (var node in allNodes.OfType<AbstractEventNode>().Where(node => node.IsBaseEventNode)) {
                code.AppendLine(node.GetCode());
            }
            
            // CreateMethod Nodes
            foreach (var node in allNodes.OfType<CreateMethodNode>()) {
                code.AppendLine(node.GetCode());
            }

            code.Append("}");
            return code.ToString();
        }

        private List<CompilerError> CompileCode(string code) {
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => a.Location);
            var parameters = new CompilerParameters {GenerateExecutable = false, IncludeDebugInformation = true, TreatWarningsAsErrors = false, WarningLevel = 1};
            parameters.ReferencedAssemblies.AddRange(assemblies.ToArray());
            var results = provider.CompileAssemblyFromSource(parameters, code);
            var errors = new List<CompilerError>();
            if (results.Errors.Count <= 0)
                return errors;
            errors.AddRange(results.Errors.Cast<CompilerError>());
            return errors;
        }

        private string WriteCodeToFile(string code) {
            var graphPath = GraphObject.CodeGraphData.AssetPath;
            var graphFileIndex = graphPath.LastIndexOf("/", StringComparison.Ordinal);
            var graphPath2 = graphPath.Substring(0, graphFileIndex + 1);
            var assetPath = graphPath2 + GraphObject.CodeGraphData.GraphName.Replace(" ", "") + ".cs";
            File.WriteAllText(assetPath, code);
            return assetPath;
        }

        private void GenerateMiniMap() {
            return;
            var miniMap = new MiniMap {anchored = true};
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            GraphView.Add(miniMap);
        }

        private void OnEnable() {
            rootVisualElement.AddStyleSheet("CodeGraphWindow");
            GenerateToolbar();
            ConstructGraphView();
            toolbar.BringToFront();
            ValidateSaveButton();
            GenerateMiniMap();
        }

        private void OnDisable() {
            rootVisualElement.Remove(GraphView);
        }
    }
}