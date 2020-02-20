using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public abstract class CodeNode : Node, IGeneratesCode {
        protected readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        public bool IsEntryPoint = false; 
        public string GUID;
        public List<Port> InputPorts = new List<Port>();
        public List<Port> OutputPorts = new List<Port>();
        public abstract string GetCode();

        protected Port AddPort(Port port, bool isInput = true) {
            if (isInput) {
                inputContainer.Add(port);
                InputPorts.Add(port);
            } else {
                outputContainer.Add(port);
                OutputPorts.Add(port);
            }

            return port;
        }

        protected CodeNode GetNodeFromPort(int portIndex, bool isInput = true) {
            var list = isInput ? InputPorts : OutputPorts;
            if (portIndex >= list.Count) return null;
            var port = list[portIndex];
            if (port.connections.ToList().Count == 0) return null;
            return port.connections.ToList()[0].output.node as CodeNode;
        }
    }
}