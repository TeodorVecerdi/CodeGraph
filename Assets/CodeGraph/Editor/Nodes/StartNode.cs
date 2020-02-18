using System;
using CodeGraph.Types;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public class StartNode : CodeNode {
        public StartNode() {
            base.title = "START";
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
            var port = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(AnyType));
            port.portName = "Code";
            port.portColor = new Color(0.27f, 0.4f, 0.91f);
            var label = new Label();
            label.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            label.text = "Connect this node to any other\nnode you would like to be put\nin the Start method of the\ngenerated MonoBehaviour.";
            outputContainer.Add(label);
            outputContainer.Add(port);

            RefreshExpandedState();
            RefreshPorts();
            base.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
        }
    }
}