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
            var remappedNodes = new List<AbstractNode>();
            foreach (var node in copyGraph.Nodes) {
                var oldGuid = node.GUID;
                var newGuid = Guid.NewGuid().ToString();
                nodeGuidMap[oldGuid] = newGuid;
                node.GUID = newGuid;
                remappedNodes.Add(node);
            }

            // Compute the mean of the copied nodes.
            var centroid = Vector2.zero;
            var count = 1;
            foreach (var node in remappedNodes) {
                var position = node.GetPosition().position;
                centroid += (position - centroid) / count;
                ++count;
            }
            // Get the center of the current view
            var viewCenter = graphView.contentViewContainer.WorldToLocal(graphView.layout.center);
            foreach (var node in remappedNodes) {
                var positionRect = node.GetPosition();
                var position = positionRect.position;
                position += viewCenter - centroid;
                positionRect.position = position;
                node.SetPosition(positionRect);
            }
            
            remappedNodes.ForEach(graphView.AddElement);
            // remappedEdges.ForEach(edge => Debug.Log($"edge input: {(edge.input.node as AbstractNode).GUID}, edge output: {(edge.output.node as AbstractNode).GUID}"));
            copyGraph.Edges.ToList().ForEach(graphView.Add);

            // Add new elements to selection
            graphView.ClearSelection();
            copyGraph.Edges.ToList().ForEach(graphView.AddToSelection);
            remappedNodes.ForEach(graphView.AddToSelection);
            /*graphView.graphElements.ForEach(element => {
                if (element is Edge edge && copyGraph.Edges.Contains(edge))
                    graphView.AddToSelection(edge);

                if (element is AbstractNode nodeView && remappedNodes.Contains(nodeView))
                    graphView.AddToSelection(nodeView);
            });*/
        }
    }
}