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
        private CodeGraphView graphView;
        private CodeGraphObject graphObject;

        [MenuItem("Graph/Code Graph")]
        public static void CreateGraphViewWindow() {
            var window = GetWindow<CodeGraph>();
            window.titleContent = new GUIContent("CodeGraph Editor", Resources.Load<Texture2D>("codegraph_256"));
        }

        public void SetGraph(CodeGraphObject graphObject) {
            this.graphObject = graphObject;
        }

        public void Initialize() {
            LoadGraph();
        }

        public void LoadGraph() {
            SaveUtility.GetInstance(graphView).LoadGraph(graphObject);
        }

        public void SaveGraph() {
            var newGraphObject = SaveUtility.GetInstance(graphView).Save(graphObject.CodeGraphData.AssetPath);
            graphObject = newGraphObject;
        }

        private void ConstructGraphView() {
            graphView = new CodeGraphView(this) {
                name = "Code Graph"
            };
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            if (graphObject != null) {
                SaveUtility.GetInstance(graphView).LoadGraph(graphObject);
            }
        }

        private void GenerateToolbar() {
            var toolbar = new Toolbar();
            toolbar.Add(new Button(() => SaveGraph()) {text = "Save Graph"});
            toolbar.Add(new Button(() => {
                var code = GenerateCode();
                var errors = CompileCode(code);
                if (errors.Any()) {
                    errors.ForEach(error=>Debug.LogError($"Error {error.ErrorNumber} => \"{error.ErrorText} at line number {error.Line}\"\r\n"));
                    return;
                }
                var assetPath = WriteCodeToFile(code);
                AssetDatabase.ImportAsset(assetPath);
            }) {text = "Compile Graph"});
            toolbar.Add(new Button(() => Debug.Log(GenerateCode())) {text = "Print Code"});
            toolbar.Add(new Button(() => Debug.Log(graphView.contentViewContainer.worldBound)) {text = "World Bound"});
            // toolbar.Add(new Button(() => Debug.Log(graphView.contentViewContainer.)) {text = "Layout"});
            // toolbar.Add(new Button(() => Debug.Log(graphView[0].worldBound)) {text = "World Bound"});
            // toolbar.Add(new Button(() => Debug.Log(graphView[0].contentRect)) {text = "Content Rect"});
            // toolbar.Add(new Button(() => Debug.Log(graphView[0].layout)) {text = "Layout"});
            rootVisualElement.Add(toolbar);
        }

        private string GenerateCode() {
            var monobehaviourName = graphObject.CodeGraphData.GraphName;
            monobehaviourName = monobehaviourName.Replace(" ", "");
            var code = new StringBuilder();
            code.AppendLine($"using UnityEngine;\npublic class {monobehaviourName} : MonoBehaviour {{");
            
            // Property Nodes
            foreach (var node in graphView.nodes.ToList()) {
                if (node is CreatePropertyNode propertyNode) {
                    code.AppendLine(propertyNode.GetCode());
                }
            }
            // Event Nodes
            foreach (var node in graphView.nodes.ToList()) {
                if (node is AbstractEventNode eventNode && eventNode.IsBaseEventNode) {
                    code.AppendLine(eventNode.GetCode());
                }
            }
            // var eventNodes = (from node in graphView.nodes.ToList().AsEnumerable() select node into eventNode where (AbstractEventNode) eventNode != null && ((AbstractEventNode) eventNode).IsBaseEventNode select eventNode).ToList();
            // eventNodes.ForEach(node => code +=  ((AbstractEventNode)node).GetCode() + "\n");
            // code += graphView.StartEventNode.GetCode() + "\n";
            // code += graphView.UpdateEventNode.GetCode() + "\n";
            code.Append("}");
            // Debug.Log(code);
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
            var graphPath = graphObject.CodeGraphData.AssetPath;
            var graphFileIndex = graphPath.LastIndexOf("/", StringComparison.Ordinal);
            var graphPath2 = graphPath.Substring(0, graphFileIndex+1);
            var assetPath = graphPath2 + graphObject.CodeGraphData.GraphName.Replace(" ", "") + ".cs";
            File.WriteAllText(assetPath, code);
            return assetPath;
        }
        

        private void GenerateMiniMap() {
            return;
            var miniMap = new MiniMap {anchored = true};
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            graphView.Add(miniMap);
        }

        private void OnEnable() {
            ConstructGraphView();
            GenerateToolbar();
            GenerateMiniMap();
        }

        private void OnDisable() {
            rootVisualElement.Remove(graphView);
        }
    }
}