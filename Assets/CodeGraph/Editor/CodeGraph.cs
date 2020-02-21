using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class CodeGraph : EditorWindow {
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
            graphView = new CodeGraphView {
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
            toolbar.Add(new Button(() => GenerateClass()) {text = "Compile Graph"});
            rootVisualElement.Add(toolbar);
        }

        private void GenerateClass() {
            var monobehaviourName = graphObject.CodeGraphData.GraphName;
            monobehaviourName = monobehaviourName.Replace(" ", "");
            var @class = $"using UnityEngine;\npublic class {monobehaviourName} : MonoBehaviour {{\n";
            @class += graphView.StartEventNode.GetCode() + "\n";
            @class += graphView.UpdateEventNode.GetCode() + "\n";
            @class += "}";
            Debug.Log(@class);
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