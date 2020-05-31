using System;

namespace CodeGraph.Editor {
    [AttributeUsage( AttributeTargets.Class)]
    public class TitleAttribute : Attribute {
        public string[] title;

        public TitleAttribute(params string[] title) {
            this.title = title;
        }
    }
}