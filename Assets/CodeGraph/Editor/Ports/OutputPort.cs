using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public class OutputPort {
        public event Func<string> GetCodeEvent;
        public AbstractNode ParentNode;
        public Port PortReference;

        public OutputPort(AbstractNode parentNode, Port portReference, Func<string> getCode) {
            ParentNode = parentNode;
            PortReference = portReference;
            GetCodeEvent += getCode;
        }
        
        public string GetCode() {
            return GetCodeEvent != null ? GetCodeEvent() : "/** ERROR **/";
        }
    }
}