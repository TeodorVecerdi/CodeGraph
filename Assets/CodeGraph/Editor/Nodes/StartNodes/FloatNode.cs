using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class FloatNode : AbstractStartNode {
        private float value = 0.0f;

        public FloatNode() {
            Initialize("Float", DefaultNodePosition);
            var floatField = new FloatField {label = "x:", value = 0};
            floatField.labelElement.style.minWidth = 0;
            floatField.RegisterValueChangedCallback(evt => value = evt.newValue);
            inputContainer.Add(floatField);

            var valuePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            valuePort.portName = "value";
            AddOutputPort(valuePort, () => $"{value}f");
            Refresh();
        }
    }
}