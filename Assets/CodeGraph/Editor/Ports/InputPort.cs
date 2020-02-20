using System;
using UnityEditor.Experimental.GraphView;

namespace CodeGraph.Editor {
    public class InputPort {
        public event Func<string> RequestCodeEvent;
        public AbstractNode ParentNode;
        public Port PortReference;

        public InputPort(AbstractNode parentNode, Port portReference, Func<string> requestCode) {
            ParentNode = parentNode;
            PortReference = portReference;
            RequestCodeEvent += requestCode;
        }

        public string RequestCode() {
            return RequestCodeEvent != null ? RequestCodeEvent() : "/** ERROR **/";
        }
    }
}