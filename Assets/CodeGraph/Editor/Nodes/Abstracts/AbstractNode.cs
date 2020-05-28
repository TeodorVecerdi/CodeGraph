using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class AbstractNode : Node, IGroupItem {
        public static readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        protected static readonly Rect DefaultNodePosition = new Rect(Vector2.zero, DefaultNodeSize);

        public string GUID;
        public List<InputPort> InputPorts = new List<InputPort>();
        public List<OutputPort> OutputPorts = new List<OutputPort>();
        public Dictionary<Port, InputPort> InputPortDictionary = new Dictionary<Port, InputPort>();
        public Dictionary<Port, OutputPort> OutputPortDictionary = new Dictionary<Port, OutputPort>();
        protected string GetDebugData => $"//BEGIN_NODE_GUID/{GUID}/END_NODE_GUID";

        protected void AddInputPort(Port portReference, Func<string> requestCode, bool alsoAddToHierarchy = true) {
            var inputPort = new InputPort(this, portReference, requestCode);
            InputPorts.Add(inputPort);
            InputPortDictionary.Add(portReference, inputPort);
            if(alsoAddToHierarchy) inputContainer.Add(portReference);
        }

        protected void AddOutputPort(Port portReference, Func<string> getCode, bool alsoAddToHierarchy = true) {
            var outputPort = new OutputPort(this, portReference, getCode); 
            OutputPorts.Add(outputPort);
            OutputPortDictionary.Add(portReference, outputPort);
            if(alsoAddToHierarchy) outputContainer.Add(portReference);
        }

        protected void Initialize(string nodeTitle, Rect position) {
            base.title = nodeTitle;
            base.SetPosition(position);
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
        }

        public void Refresh() {
            RefreshPorts();
            RefreshExpandedState();
        }

        public virtual void SetNodeData(string jsonData) {
            var root = JObject.Parse(jsonData);
            base.expanded = root.Value<bool>("expanded");
        }

        public virtual string GetNodeData() {
            var root = new JObject();
            root["expanded"] = base.expanded;
            return root.ToString(Formatting.None);
        }

        public virtual void OnCreateFromSearchWindow(Vector2 nodePosition) {}
        
        protected static T CreateAndConnectNode<T>(Rect position, int outputIndex, int inputIndex, AbstractNode parent) where T : AbstractNode, new() {
            var node = new T();
            node.SetPosition(position);
            CodeGraph.Instance.GraphView.AddElement(node);
            var edge = new Edge {
                output = node.OutputPorts[outputIndex].PortReference,
                input = parent.InputPorts[inputIndex].PortReference
            };
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            CodeGraph.Instance.GraphView.Add(edge);
            node.Refresh();
            return node;
        }

        public Guid GroupGuid { get; set; }
    }
}