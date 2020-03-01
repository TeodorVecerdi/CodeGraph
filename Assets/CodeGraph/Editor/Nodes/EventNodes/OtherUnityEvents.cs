using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Title("Events", "OnAnimatorMove Event")]
    public class OnAnimatorMoveEventNode : AbstractEventNode {
        public OnAnimatorMoveEventNode() {
            Initialize("OnAnimatorMove", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnAnimatorMove() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnApplicationQuit Event")]
    public class OnApplicationQuitEventNode : AbstractEventNode {
        public OnApplicationQuitEventNode() {
            Initialize("OnApplicationQuit", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnApplicationQuit() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnBecameInvisible Event")]
    public class OnBecameInvisibleEventNode : AbstractEventNode {
        public OnBecameInvisibleEventNode() {
            Initialize("OnBecameInvisible", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBecameInvisible() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnBecameVisible Event")]
    public class OnBecameVisibleEventNode : AbstractEventNode {
        public OnBecameVisibleEventNode() {
            Initialize("OnBecameVisible", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBecameVisible() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnBeforeTransformParentChanged Event")]
    public class OnBeforeTransformParentChangedEventNode : AbstractEventNode {
        public OnBeforeTransformParentChangedEventNode() {
            Initialize("OnBeforeTransformParentChanged", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBeforeTransformParentChanged() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnCanvasGroupChanged Event")]
    public class OnCanvasGroupChangedEventNode : AbstractEventNode {
        public OnCanvasGroupChangedEventNode() {
            Initialize("OnCanvasGroupChanged", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnCanvasGroupChanged() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnCanvasHierarchyChanged Event")]
    public class OnCanvasHierarchyChangedEventNode : AbstractEventNode {
        public OnCanvasHierarchyChangedEventNode() {
            Initialize("OnCanvasHierarchyChanged", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnCanvasHierarchyChanged() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnDestroy Event")]
    public class OnDestroyEventNode : AbstractEventNode {
        public OnDestroyEventNode() {
            Initialize("OnDestroy", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDestroy() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnDidApplyAnimationProperties Event")]
    public class OnDidApplyAnimationPropertiesEventNode : AbstractEventNode {
        public OnDidApplyAnimationPropertiesEventNode() {
            Initialize("OnDidApplyAnimationProperties", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDidApplyAnimationProperties() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnDisable Event")]
    public class OnDisableEventNode : AbstractEventNode {
        public OnDisableEventNode() {
            Initialize("OnDisable", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDisable() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnDrawGizmos Event")]
    public class OnDrawGizmosEventNode : AbstractEventNode {
        public OnDrawGizmosEventNode() {
            Initialize("OnDrawGizmos", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDrawGizmos() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnDrawGizmosSelected Event")]
    public class OnDrawGizmosSelectedEventNode : AbstractEventNode {
        public OnDrawGizmosSelectedEventNode() {
            Initialize("OnDrawGizmosSelected", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDrawGizmosSelected() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnEnable Event")]
    public class OnEnableEventNode : AbstractEventNode {
        public OnEnableEventNode() {
            Initialize("OnEnable", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnEnable() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnGUI Event")]
    public class OnGUIEventNode : AbstractEventNode {
        public OnGUIEventNode() {
            Initialize("OnGUI", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnGUI() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseDown Event")]
    public class OnMouseDownEventNode : AbstractEventNode {
        public OnMouseDownEventNode() {
            Initialize("OnMouseDown", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseDown() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseDrag Event")]
    public class OnMouseDragEventNode : AbstractEventNode {
        public OnMouseDragEventNode() {
            Initialize("OnMouseDrag", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseDrag() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseEnter Event")]
    public class OnMouseEnterEventNode : AbstractEventNode {
        public OnMouseEnterEventNode() {
            Initialize("OnMouseEnter", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseEnter() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseExit Event")]
    public class OnMouseExitEventNode : AbstractEventNode {
        public OnMouseExitEventNode() {
            Initialize("OnMouseExit", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseExit() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseOver Event")]
    public class OnMouseOverEventNode : AbstractEventNode {
        public OnMouseOverEventNode() {
            Initialize("OnMouseOver", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseOver() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseUp Event")]
    public class OnMouseUpEventNode : AbstractEventNode {
        public OnMouseUpEventNode() {
            Initialize("OnMouseUp", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseUp() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnMouseUpAsButton Event")]
    public class OnMouseUpAsButtonEventNode : AbstractEventNode {
        public OnMouseUpAsButtonEventNode() {
            Initialize("OnMouseUpAsButton", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseUpAsButton() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnParticleSystemStopped Event")]
    public class OnParticleSystemStoppedEventNode : AbstractEventNode {
        public OnParticleSystemStoppedEventNode() {
            Initialize("OnParticleSystemStopped", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleSystemStopped() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnParticleTrigger Event")]
    public class OnParticleTriggerEventNode : AbstractEventNode {
        public OnParticleTriggerEventNode() {
            Initialize("OnParticleTrigger", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleTrigger() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnParticleUpdateJobScheduled Event")]
    public class OnParticleUpdateJobScheduledEventNode : AbstractEventNode {
        public OnParticleUpdateJobScheduledEventNode() {
            Initialize("OnParticleUpdateJobScheduled", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleUpdateJobScheduled() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnPostRender Event")]
    public class OnPostRenderEventNode : AbstractEventNode {
        public OnPostRenderEventNode() {
            Initialize("OnPostRender", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPostRender() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnPreCull Event")]
    public class OnPreCullEventNode : AbstractEventNode {
        public OnPreCullEventNode() {
            Initialize("OnPreCull", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPreCull() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnPreRender Event")]
    public class OnPreRenderEventNode : AbstractEventNode {
        public OnPreRenderEventNode() {
            Initialize("OnPreRender", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPreRender() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnRectTransformDimensionsChange Event")]
    public class OnRectTransformDimensionsChangeEventNode : AbstractEventNode {
        public OnRectTransformDimensionsChangeEventNode() {
            Initialize("OnRectTransformDimensionsChange", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnRectTransformDimensionsChange() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnRenderObject Event")]
    public class OnRenderObjectEventNode : AbstractEventNode {
        public OnRenderObjectEventNode() {
            Initialize("OnRenderObject", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnRenderObject() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnTransformChildrenChanged Event")]
    public class OnTransformChildrenChangedEventNode : AbstractEventNode {
        public OnTransformChildrenChangedEventNode() {
            Initialize("OnTransformChildrenChanged", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnTransformChildrenChanged() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnTransformParentChanged Event")]
    public class OnTransformParentChangedEventNode : AbstractEventNode {
        public OnTransformParentChangedEventNode() {
            Initialize("OnTransformParentChanged", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnTransformParentChanged() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnValidate Event")]
    public class OnValidateEventNode : AbstractEventNode {
        public OnValidateEventNode() {
            Initialize("OnValidate", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnValidate() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "OnWillRenderObject Event")]
    public class OnWillRenderObjectEventNode : AbstractEventNode {
        public OnWillRenderObjectEventNode() {
            Initialize("OnWillRenderObject", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnWillRenderObject() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Title("Events", "Reset Event")]
    public class ResetEventNode : AbstractEventNode {
        public ResetEventNode() {
            Initialize("Reset", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void Reset() {");
            var nodes = (from outputPort in OutputPorts
                select outputPort.PortReference.connections.ToList()
                into connections
                where connections.Count != 0
                select connections[0].input.node).ToList();

            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if (eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            code.AppendLine("}");
            return code.ToString();
        }
    }
}