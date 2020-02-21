using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class AbstractNode : Node {
        public static readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        protected static readonly Rect DefaultNodePosition = new Rect(Vector2.zero, DefaultNodeSize);

        public string GUID;
        public List<InputPort> InputPorts = new List<InputPort>();
        public List<OutputPort> OutputPorts = new List<OutputPort>();
        public Dictionary<Port, InputPort> InputPortDictionary = new Dictionary<Port, InputPort>();
        public Dictionary<Port, OutputPort> OutputPortDictionary = new Dictionary<Port, OutputPort>();
        protected string GetDebugData => $"//BEGIN_NODE_GUID/{GUID}/END_NODE_GUID";

        protected void AddInputPort(Port portReference, Func<string> requestCode) {
            var inputPort = new InputPort(this, portReference, requestCode);
            InputPorts.Add(inputPort);
            InputPortDictionary.Add(portReference, inputPort);
            inputContainer.Add(portReference);
        }

        protected void AddOutputPort(Port portReference, Func<string> getCode) {
            var outputPort = new OutputPort(this, portReference, getCode); 
            OutputPorts.Add(outputPort);
            OutputPortDictionary.Add(portReference, outputPort);
            outputContainer.Add(portReference);
        }

        protected void Initialize(string nodeTitle, Rect position) {
            base.title = nodeTitle;
            base.SetPosition(position);
            GUID = Guid.NewGuid().ToString();
            this.AddStyleSheet("CodeNode");
        }

        protected void Refresh() {
            RefreshExpandedState();
            RefreshPorts();
        }

        public abstract void SetNodeData(string jsonData);
        public abstract string GetNodeData();

    }
}