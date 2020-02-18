using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class Vector2Node : CodeNode {
        public Vector2Node() {
            base.title = "Vector2";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Vector2));
            port.portName = "(2)";
            outputContainer.Add(port);

            var ip1 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip1.portName = "x";
            var ip2 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip2.portName = "y";
            
            inputContainer.Add(ip1);
            inputContainer.Add(ip2);

            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}