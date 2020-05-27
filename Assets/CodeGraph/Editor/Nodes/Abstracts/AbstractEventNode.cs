using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class AbstractEventNode : AbstractNode {
        public int PortCount;
        public bool IsBaseEventNode;

        protected AbstractEventNode() {
            // capabilities &= ~Capabilities.Deletable;
            IsBaseEventNode = true;
        }

        [Obsolete("Event nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) {
            throw new NotSupportedException("Event nodes cannot have Input ports");
        }

        public void AddChildPort(bool incrementPortCount = true) {
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = $"child {OutputPorts.Count + 1}";
            outputPort.portColor = new Color(1, 1, 1, 0.2f);

            // AddOutputPort(outputPort, () => "");
            var outputPortObj = new OutputPort(this, outputPort, () => "");
            OutputPorts.Add(outputPortObj);
            OutputPortDictionary.Add(outputPort, outputPortObj);
            outputPort.contentContainer.Add(new Button(() => { RemovePort(outputPortObj);}){text = "X"});
            outputContainer.Add(outputPort);

            if (incrementPortCount) PortCount++;
            Refresh();
        }

        private void RemovePort(OutputPort port) {
            var connectionsToRemove = new List<Edge>();
            connectionsToRemove.AddRange(port.PortReference.connections);
            while(connectionsToRemove.Count > 0) {
                var conn = connectionsToRemove[0];
                CodeGraph.Instance.GraphView.RemoveElement(conn);
                conn.input.Disconnect(conn);
                conn.output.Disconnect(conn);
                // conn = null;
                connectionsToRemove.RemoveAt(0);
            }
            outputContainer.Remove(port.PortReference);
            port.PortReference = null;
            OutputPorts.Remove(port);
        }

        public void CleanPorts() {
            var portsToRemove = new List<OutputPort>();
            (from port in OutputPorts
                    let portConnections = port.PortReference.connections.ToList()
                    where portConnections.Count == 0
                    select port).ToList()
                .ForEach(port => {
                    outputContainer.Remove(port.PortReference);
                    port.PortReference = null;
                    portsToRemove.Add(port);
                    PortCount--;
                });
            portsToRemove.ForEach(p => OutputPorts.Remove(p));
            var i = 1;
            OutputPorts.ForEach(port => {
                port.PortReference.portName = $"child {i}";
                i++;
            });
            Refresh();
        }

        public abstract string GetCode();
    }
}