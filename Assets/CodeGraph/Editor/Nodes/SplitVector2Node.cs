using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    public class SplitVector2Node : CodeNode {
        public SplitVector2Node() {
            base.title = "Split Vector2";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var ip1 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Vector2));
            ip1.portName = "(2)";
            inputContainer.Add(ip1);
            
            var op1 = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            op1.portName = "x";
            var op2 = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            op2.portName = "y";
            outputContainer.Add(op1);
            outputContainer.Add(op2);

            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}