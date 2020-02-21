using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    public abstract class AbstractEventNode : AbstractNode {
        public int PortCount;
        public bool IsBaseEventNode;
        protected AbstractEventNode() {
            capabilities &= ~Capabilities.Deletable;
            IsBaseEventNode = true;
        }
        [Obsolete("Event nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) {
            throw new NotSupportedException("Event nodes cannot have Input ports");
        }
        
        public void AddChildPort(bool incrementPortCount = true) {
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = $"Child {OutputPorts.Count + 1}";
            outputPort.portColor = new Color(0.53f, 0f, 0.67f);
            AddOutputPort(outputPort, () => "");
            if (incrementPortCount) PortCount++;
            Refresh();
        }

        public abstract string GetCode();
    }
}