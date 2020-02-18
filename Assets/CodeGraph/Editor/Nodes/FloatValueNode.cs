using System;
using CodeGraph.Types;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class FloatValueNode : CodeNode {
        private float value = 0;
        public FloatValueNode() {
            base.title = "Float";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            port.portName = "(1)";
            port.portColor = new Color(0.518f, 0.894f, 0.906f, 1.000f);
            outputContainer.Add(port);

            var floatField = new FloatField {label = "x:", value = 0};
            floatField.labelElement.style.minWidth = 0;
            floatField.RegisterValueChangedCallback(evt => value = evt.newValue);
            inputContainer.Add(floatField);
            
            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}