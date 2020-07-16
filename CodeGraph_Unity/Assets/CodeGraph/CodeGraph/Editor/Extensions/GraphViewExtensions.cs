using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.Editor {
    public static class GraphViewExtensions {
        public static void InsertCopyPasteGraph(this CodeGraphView graphView, CopyPasteGraphData copyGraph) {
            if (copyGraph == null)
                return;

            var nodeGuidMap = new Dictionary<string, string>();
            var nodeGuidMapReverse = new Dictionary<string, string>();
            var remappedNodes = new List<AbstractNode>();
            foreach (var node in copyGraph.Nodes) {
                var oldGuid = node.GUID;
                var newGuid = Guid.NewGuid().ToString();
                nodeGuidMap[newGuid] = oldGuid;
                nodeGuidMapReverse[oldGuid] = newGuid;
                node.GUID = newGuid;
                remappedNodes.Add(node);
            }

            // Compute the mean of the copied nodes.
            var centroid = Vector2.zero;
            var count = 1;
            foreach (var node in remappedNodes) {
                var position = copyGraph.NodePositions[nodeGuidMap[node.GUID]].position;
                centroid += (position - centroid) / count;
                ++count;
            }

            // Get the center of the current view
            var viewCenter = graphView.contentViewContainer.WorldToLocal(graphView.layout.center);
            foreach (var node in remappedNodes) {
                var positionRect = copyGraph.NodePositions[nodeGuidMap[node.GUID]];
                var position = positionRect.position;
                position += viewCenter - centroid;
                positionRect.position = position;
                node.SetPosition(positionRect);
            }

            remappedNodes.ForEach(graphView.AddElement);
            copyGraph.Edges.ToList().ForEach(graphView.Add);

            // Add new elements to selection
            graphView.ClearSelection();
            copyGraph.Edges.ToList().ForEach(graphView.AddToSelection);
            remappedNodes.ForEach(graphView.AddToSelection);
        }
    }
}