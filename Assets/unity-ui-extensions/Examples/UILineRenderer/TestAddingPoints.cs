using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.UILineRenderer
{
    public class TestAddingPoints : MonoBehaviour
    {
        public Scripts.Primitives.UILineRenderer LineRenderer;
        public Text XValue;
        public Text YValue;

        // Use this for initialization
        public void AddNewPoint()
        {
            var point = new Vector2() { x = float.Parse(XValue.text), y = float.Parse(YValue.text) };
            var pointlist = new List<Vector2>(LineRenderer.Points);
            pointlist.Add(point);
            LineRenderer.Points = pointlist.ToArray();
        }

        public void ClearPoints()
        {
            LineRenderer.Points = new Vector2[0];
        }
    }
}