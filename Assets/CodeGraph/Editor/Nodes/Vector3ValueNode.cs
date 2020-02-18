using System;
using CodeGraph.Types;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class Vector3ValueNode : CodeNode {
        private Vector3 value = Vector3.zero;
        public Vector3ValueNode() {
            base.title = "Vector3";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Vector3));
            port.portName = "(3)";
            outputContainer.Add(port);

            var floatField = new FloatField {label = "x:", value = 0};
            floatField.labelElement.style.minWidth = 0;
            floatField.RegisterValueChangedCallback(evt => value.x = evt.newValue);
            inputContainer.Add(floatField);
            
            var floatField2 = new FloatField {label = "y:", value = 0};
            floatField2.labelElement.style.minWidth = 0;
            floatField2.RegisterValueChangedCallback(evt => value.y = evt.newValue);
            inputContainer.Add(floatField2);
            
            var floatField3 = new FloatField {label = "z:", value = 0};
            floatField3.labelElement.style.minWidth = 0;
            floatField3.RegisterValueChangedCallback(evt => value.z = evt.newValue);
            inputContainer.Add(floatField3);
            
            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}