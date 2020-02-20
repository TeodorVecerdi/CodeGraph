using System;
using System.Linq;
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
            AddPort(port, false);

            var ip1 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip1.portName = "x";
            ip1.name = "port_x";
            var ip2 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip2.portName = "y";
            ip2.name = "port_y";
            var ip3 = base.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            ip3.portName = "z";
            ip3.name = "port_z";
            AddPort(ip1);
            AddPort(ip2);
            AddPort(ip3);
            titleButtonContainer.Add(new Button(() => Debug.Log(GetCode())) {text = "GetCode"});
            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }

        public override string GetCode() {
            var node1 = GetNodeFromPort(0);
            var node2 = GetNodeFromPort(1);
            var node3 = GetNodeFromPort(2);
            if (node1 == null || node2 == null || node3 == null) {
                return "some port is null";
            }
            return $"new Vector3({node1.GetCode()}, {node2.GetCode()}, {node3.GetCode()})";
        }
    }
}