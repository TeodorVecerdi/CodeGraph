using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    [Node(false, true)]
    [Title("Events", "OnAnimatorMove Event")]
    public class OnAnimatorMoveEventNode : AbstractEventNode {
        public OnAnimatorMoveEventNode() {
            Initialize("OnAnimatorMove", DefaultNodePosition);
            // titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnAnimatorMove() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnApplicationQuit Event")]
    public class OnApplicationQuitEventNode : AbstractEventNode {
        public OnApplicationQuitEventNode() {
            Initialize("OnApplicationQuit", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnApplicationQuit() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnBecameInvisible Event")]
    public class OnBecameInvisibleEventNode : AbstractEventNode {
        public OnBecameInvisibleEventNode() {
            Initialize("OnBecameInvisible", DefaultNodePosition);
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBecameInvisible() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnBecameVisible Event")]
    public class OnBecameVisibleEventNode : AbstractEventNode {
        public OnBecameVisibleEventNode() {
            Initialize("OnBecameVisible", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBecameVisible() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnBeforeTransformParentChanged Event")]
    public class OnBeforeTransformParentChangedEventNode : AbstractEventNode {
        public OnBeforeTransformParentChangedEventNode() {
            Initialize("OnBeforeTransformParentChanged", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnBeforeTransformParentChanged() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnCanvasGroupChanged Event")]
    public class OnCanvasGroupChangedEventNode : AbstractEventNode {
        public OnCanvasGroupChangedEventNode() {
            Initialize("OnCanvasGroupChanged", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnCanvasGroupChanged() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnCanvasHierarchyChanged Event")]
    public class OnCanvasHierarchyChangedEventNode : AbstractEventNode {
        public OnCanvasHierarchyChangedEventNode() {
            Initialize("OnCanvasHierarchyChanged", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnCanvasHierarchyChanged() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnDestroy Event")]
    public class OnDestroyEventNode : AbstractEventNode {
        public OnDestroyEventNode() {
            Initialize("OnDestroy", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDestroy() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnDidApplyAnimationProperties Event")]
    public class OnDidApplyAnimationPropertiesEventNode : AbstractEventNode {
        public OnDidApplyAnimationPropertiesEventNode() {
            Initialize("OnDidApplyAnimationProperties", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDidApplyAnimationProperties() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnDisable Event")]
    public class OnDisableEventNode : AbstractEventNode {
        public OnDisableEventNode() {
            Initialize("OnDisable", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDisable() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnDrawGizmos Event")]
    public class OnDrawGizmosEventNode : AbstractEventNode {
        public OnDrawGizmosEventNode() {
            Initialize("OnDrawGizmos", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDrawGizmos() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnDrawGizmosSelected Event")]
    public class OnDrawGizmosSelectedEventNode : AbstractEventNode {
        public OnDrawGizmosSelectedEventNode() {
            Initialize("OnDrawGizmosSelected", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnDrawGizmosSelected() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnEnable Event")]
    public class OnEnableEventNode : AbstractEventNode {
        public OnEnableEventNode() {
            Initialize("OnEnable", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnEnable() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnGUI Event")]
    public class OnGUIEventNode : AbstractEventNode {
        public OnGUIEventNode() {
            Initialize("OnGUI", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnGUI() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseDown Event")]
    public class OnMouseDownEventNode : AbstractEventNode {
        public OnMouseDownEventNode() {
            Initialize("OnMouseDown", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseDown() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseDrag Event")]
    public class OnMouseDragEventNode : AbstractEventNode {
        public OnMouseDragEventNode() {
            Initialize("OnMouseDrag", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseDrag() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseEnter Event")]
    public class OnMouseEnterEventNode : AbstractEventNode {
        public OnMouseEnterEventNode() {
            Initialize("OnMouseEnter", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseEnter() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseExit Event")]
    public class OnMouseExitEventNode : AbstractEventNode {
        public OnMouseExitEventNode() {
            Initialize("OnMouseExit", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseExit() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseOver Event")]
    public class OnMouseOverEventNode : AbstractEventNode {
        public OnMouseOverEventNode() {
            Initialize("OnMouseOver", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseOver() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseUp Event")]
    public class OnMouseUpEventNode : AbstractEventNode {
        public OnMouseUpEventNode() {
            Initialize("OnMouseUp", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseUp() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnMouseUpAsButton Event")]
    public class OnMouseUpAsButtonEventNode : AbstractEventNode {
        public OnMouseUpAsButtonEventNode() {
            Initialize("OnMouseUpAsButton", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnMouseUpAsButton() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnParticleSystemStopped Event")]
    public class OnParticleSystemStoppedEventNode : AbstractEventNode {
        public OnParticleSystemStoppedEventNode() {
            Initialize("OnParticleSystemStopped", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleSystemStopped() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnParticleTrigger Event")]
    public class OnParticleTriggerEventNode : AbstractEventNode {
        public OnParticleTriggerEventNode() {
            Initialize("OnParticleTrigger", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleTrigger() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnParticleUpdateJobScheduled Event")]
    public class OnParticleUpdateJobScheduledEventNode : AbstractEventNode {
        public OnParticleUpdateJobScheduledEventNode() {
            Initialize("OnParticleUpdateJobScheduled", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnParticleUpdateJobScheduled() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnPostRender Event")]
    public class OnPostRenderEventNode : AbstractEventNode {
        public OnPostRenderEventNode() {
            Initialize("OnPostRender", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPostRender() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnPreCull Event")]
    public class OnPreCullEventNode : AbstractEventNode {
        public OnPreCullEventNode() {
            Initialize("OnPreCull", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPreCull() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnPreRender Event")]
    public class OnPreRenderEventNode : AbstractEventNode {
        public OnPreRenderEventNode() {
            Initialize("OnPreRender", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnPreRender() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnRectTransformDimensionsChange Event")]
    public class OnRectTransformDimensionsChangeEventNode : AbstractEventNode {
        public OnRectTransformDimensionsChangeEventNode() {
            Initialize("OnRectTransformDimensionsChange", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnRectTransformDimensionsChange() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnRenderObject Event")]
    public class OnRenderObjectEventNode : AbstractEventNode {
        public OnRenderObjectEventNode() {
            Initialize("OnRenderObject", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnRenderObject() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnTransformChildrenChanged Event")]
    public class OnTransformChildrenChangedEventNode : AbstractEventNode {
        public OnTransformChildrenChangedEventNode() {
            Initialize("OnTransformChildrenChanged", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnTransformChildrenChanged() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnTransformParentChanged Event")]
    public class OnTransformParentChangedEventNode : AbstractEventNode {
        public OnTransformParentChangedEventNode() {
            Initialize("OnTransformParentChanged", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnTransformParentChanged() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnValidate Event")]
    public class OnValidateEventNode : AbstractEventNode {
        public OnValidateEventNode() {
            Initialize("OnValidate", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnValidate() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "OnWillRenderObject Event")]
    public class OnWillRenderObjectEventNode : AbstractEventNode {
        public OnWillRenderObjectEventNode() {
            Initialize("OnWillRenderObject", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void OnWillRenderObject() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }

    [Node(false, true)]
    [Title("Events", "Reset Event")]
    public class ResetEventNode : AbstractEventNode {
        public ResetEventNode() {
            Initialize("Reset", DefaultNodePosition);
            //titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "Get Code"});
            titleButtonContainer.Add(new Button(() => AddChildPort()) {text = "Add New Port"});
            // titleButtonContainer.Add(new Button(CleanPorts) {text = "Clean Ports"});
            Refresh();
        }

        public override string GetCode() {
            var code = new StringBuilder();
            code.AppendLine("private void Reset() {");
            code.Append(GetEventCode());
            code.AppendLine("}");
            return code.ToString();
        }
    }
}