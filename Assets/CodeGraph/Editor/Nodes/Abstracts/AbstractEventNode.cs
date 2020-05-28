using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class AbstractEventNode : AbstractNode {
        public int PortCount;
        public bool IsBaseEventNode;
        public List<OutputPort> EventPorts = new List<OutputPort>();

        protected AbstractEventNode() {
            // capabilities &= ~Capabilities.Deletable;
            IsBaseEventNode = true;
        }

        [Obsolete("Event nodes cannot have Input ports", true)]
        public new void AddInputPort(Port portReference, Func<string> requestCode) {
            throw new NotSupportedException("Event nodes cannot have Input ports");
        }

        public abstract string GetCode();

        public void AddChildPort(bool incrementPortCount = true) {
            var outputPort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.name = $"EventPort {EventPorts.Count + 1}";
            outputPort.portName = $"child {EventPorts.Count + 1}";
            outputPort.portColor = new Color(1, 1, 1, 0.2f);

            // AddOutputPort(outputPort, () => "");
            var outputPortObj = new OutputPort(this, outputPort, () => "");
            OutputPorts.Add(outputPortObj);
            EventPorts.Add(outputPortObj);
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
            OutputPorts.Remove(port);
            EventPorts.Remove(port);
            OutputPortDictionary.Remove(port.PortReference);
            
            port.PortReference = null;
        }

        public void CleanPorts() {
            var portsToRemove = new List<OutputPort>();
            (from port in EventPorts
                    let portConnections = port.PortReference.connections.ToList()
                    where portConnections.Count == 0
                    select port).ToList()
                .ForEach(port => {
                    outputContainer.Remove(port.PortReference);
                    portsToRemove.Add(port);
                    PortCount--;
                });
            portsToRemove.ForEach(p => {
                OutputPorts.Remove(p);
                EventPorts.Remove(p);
                OutputPortDictionary.Remove(p.PortReference);
                p.PortReference = null;
            });
            var i = 1;
            EventPorts.ForEach(port => {
                port.PortReference.portName = $"child {i}";
                i++;
            });
            Refresh();
        }

        public string GetEventCode() {
            var code = new StringBuilder();
            var nodes = (from outputPort in EventPorts
                    select outputPort.PortReference.connections.ToList()
                    into connections
                    where connections.Count != 0
                    select connections[0].input.node)
                .ToList();
            nodes.ForEach(node => {
                var abstractEndNode = node as AbstractEndNode;
                var eventExtenderNode = node as EventExtenderNode;
                if (abstractEndNode != null) code.AppendLine(abstractEndNode.GetCode());
                else if(eventExtenderNode != null) code.AppendLine(eventExtenderNode.GetCode());
            });
            return code.ToString();
        }


        public override string GetNodeData() {
            var root = new JObject();
            root["PortCount"] = PortCount;
            root.Merge(JObject.Parse(base.GetNodeData()));
            return root.ToString(Formatting.None);
        }

        public override void SetNodeData(string jsonData) {
            base.SetNodeData(jsonData);
            var root = JObject.Parse(jsonData);
            PortCount = root.Value<int>("PortCount");
        }
    }
}