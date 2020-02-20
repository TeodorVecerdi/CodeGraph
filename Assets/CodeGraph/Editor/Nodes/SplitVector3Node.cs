using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    public class SplitVector3Node : CodeNode {
        public SplitVector3Node() {
            base.title = "Split Vector3";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var ip1 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Vector3));
            ip1.portName = "(3)";
            inputContainer.Add(ip1);
            AddPort(ip1);
            var op1 = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            op1.portName = "x";
            var op2 = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            op2.portName = "y";
            var op3 = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            op3.portName = "z";
            AddPort(op1, false);
            AddPort(op2, false);
            AddPort(op3, false);
            
            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
        public override string GetCode() {
            return "";
        }
    }
}