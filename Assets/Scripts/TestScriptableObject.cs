using System.Collections.Generic;
using UnityEngine;

namespace CodeGraph {
    [CreateAssetMenu(fileName = "CodeGraph Item", menuName = "New CodeGraph", order = 0)]
    public class TestScriptableObject : ScriptableObject {
        public List<TestBox> Boxes = new List<TestBox>();
    }
}