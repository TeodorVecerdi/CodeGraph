using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace CodeGraph.Editor {
    public class CodeGraphView : GraphView {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);

        public CodeGraphView() {
            this.AddStyleSheet("CodeGraph");
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new FloatValueNode());
            AddElement(new Vector2Node());
            AddElement(new Vector3Node());
            AddElement(new SplitVector2Node());
            AddElement(new SplitVector3Node());
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port => {
                if (startPort != port && startPort.node != port.node && port.direction != startPort.direction) 
                    compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }

        /*public CodeNode CreateNode(string nodeName, int inputPorts = 1, int outputPorts = 1) {
            var node = new CodeNode {
                title = nodeName,
                GUID = Guid.NewGuid().ToString()
            };
            node.AddStyleSheet("CodeNode");
            for (var i = 0; i < inputPorts; i++) {
                var port = node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
                port.portName = $"Input {i + 1}";
                port.portColor = Random.ColorHSV();
                node.inputContainer.Add(port);
            }

            for (var i = 0; i < outputPorts; i++) {
                var port = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
                port.portName = $"Output {i + 1}";
                port.portColor = Random.ColorHSV();
                node.outputContainer.Add(port);
            }

            node.RefreshExpandedState();
            node.RefreshPorts();
            node.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));
            return node;
        }*/
    }
}