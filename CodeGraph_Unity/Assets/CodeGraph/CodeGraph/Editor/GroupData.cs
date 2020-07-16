using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Serializable]
    public class GroupData : ISerializationCallbackReceiver {
        public Group GroupReference;
        public Guid Guid => guid;

        public Guid RewriteGuid() {
            guid = Guid.NewGuid();
            return guid;
        }

        [NonSerialized]
        private Guid guid;

        [SerializeField]
        private string guidSerialized;

        [SerializeField]
        private string title;

        public string Title {
            get => title;
            set => title = value;
        }


        public GroupData(string title, Group groupReference) {
            guid = Guid.NewGuid();
            this.title = title;
            GroupReference = groupReference;
        }

        public void OnBeforeSerialize() {
            guidSerialized = guid.ToString();
        }

        public void OnAfterDeserialize() {
            if (!string.IsNullOrEmpty(guidSerialized)) {
                guid = new Guid(guidSerialized);
            }
        }
    }
}