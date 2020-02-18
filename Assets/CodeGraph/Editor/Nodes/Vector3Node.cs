using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class Vector3Node : CodeNode {
        public Vector3Node() {
            base.title = "Vector3";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Vector3));
            port.portName = "(3)";
            outputContainer.Add(port);

            var ip1 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip1.portName = "x";
            var ip2 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip2.portName = "y";
            var ip3 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip3.portName = "z";
            
            inputContainer.Add(ip1);
            inputContainer.Add(ip2);
            inputContainer.Add(ip3);

            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}