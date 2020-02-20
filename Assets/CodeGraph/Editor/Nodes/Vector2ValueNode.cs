using System;
using CodeGraph.Types;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class Vector2ValueNode : CodeNode {
        private Vector2 value = Vector2.zero;
        public Vector2ValueNode() {
            base.title = "Vector2";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Vector2));
            port.portName = "(2)";
            AddPort(port, false);

            var floatField = new FloatField {label = "x:", value = 0};
            floatField.labelElement.style.minWidth = 0;
            floatField.RegisterValueChangedCallback(evt => value.x = evt.newValue);
            inputContainer.Add(floatField);
            
            var floatField2 = new FloatField {label = "y:", value = 0};
            floatField2.labelElement.style.minWidth = 0;
            floatField2.RegisterValueChangedCallback(evt => value.y = evt.newValue);
            inputContainer.Add(floatField2);
            
            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }

        public override string GetCode() {
            return $"new Vector2({value.x}f,{value.y}f)";
        }
    }
}