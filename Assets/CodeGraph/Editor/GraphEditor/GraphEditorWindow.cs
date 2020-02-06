using System;
using CodeGraph.Nodes;
using UnityEditor;
using UnityEngine;

namespace CodeGraph.Editor {
    public class GraphEditorWindow : EditorWindow {
        public CodeGraphObject GraphObject;
        private const float topPanelHeight = 32f;
        private const float leftPanelWidth = 240f;
        private const float mainPanelMinWidth = 400f;
        private const float mainPanelMinHeight = 400f;
        #region Zoom Area variables
        private const float zoomMin = 0.1f;
        private const float zoomMax = 1f;
        private float zoom = 1.0f;
        private Rect zoomArea = new Rect(leftPanelWidth, topPanelHeight, mainPanelMinWidth, mainPanelMinHeight);
        private Vector2 zoomCoordsOrigin = Vector2.zero;
        private Vector2 lastWindowSize = Vector2.zero;
        #endregion

        private bool showNodeCreator;
        private NewNodeWindow newNodeWindow;

        public void Init() {
            titleContent = new GUIContent("CodeGraph Editor");
            minSize = new Vector2(leftPanelWidth + mainPanelMinWidth, topPanelHeight + mainPanelMinHeight);
            wantsMouseMove = true;
            Show();
            FocusWindowIfItsOpen<GraphEditorWindow>();
        }

        public void SetGraph(CodeGraphObject graphObject) {
            Debug.Log("Set graph " + graphObject.GraphName);
            GraphObject = graphObject;
        }

        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords) {
            return (screenCoords - zoomArea.TopLeft()) / zoom + zoomCoordsOrigin;
        }

        private void DrawZoomArea() {
            Handles.DrawSolidRectangleWithOutline(new Rect(leftPanelWidth, topPanelHeight, position.width - leftPanelWidth, position.height - topPanelHeight), new Color(0.14f, 0.14f, 0.14f), Color.clear);

            // Within the zoom area all coordinates are relative to the top left corner of the zoom area
            // with the width and height being scaled versions of the original/unzoomed area's width and height.
            EditorZoomArea.Begin(zoom, zoomArea);

            if (GraphObject != null) {
                foreach (var asset in GraphObject.Graph.GetNodes<AbstractNode>()) {
                    // TODO REMAKE
                    // GUI.Box(new Rect(asset.Position.x - zoomCoordsOrigin.x, asset.Position.y - zoomCoordsOrigin.y, asset.Size.x, asset.Size.y), asset.Title);
                }
            }

            EditorZoomArea.End();
        }

        private void DrawNonZoomArea() {
            Handles.DrawSolidRectangleWithOutline(new Rect(0, topPanelHeight, leftPanelWidth, position.height - topPanelHeight), new Color(0.13f, 0.13f, 0.13f), Color.clear);
            Handles.DrawSolidRectangleWithOutline(new Rect(0, 0, position.width, topPanelHeight), new Color(0.13f, 0.13f, 0.13f), Color.clear);
            Handles.color = new Color(0.2f, 0.2f, 0.2f);
            Handles.DrawDottedLine(new Vector3(0f, topPanelHeight), new Vector3(position.width, topPanelHeight), 2f);
            Handles.DrawDottedLine(new Vector3(leftPanelWidth, topPanelHeight), new Vector3(leftPanelWidth, position.height), 2f);

            if (GUI.Button(new Rect(0f, 0f, 100f, 32f), "Save Asset")) {
                Debug.Log(GraphObject.AssetGuid);
                GraphFileSaveManager.SaveGraphFile(AssetDatabase.GUIDToAssetPath(GraphObject.AssetGuid), GraphObject);
                AssetDatabase.Refresh();
            }

            GUI.Button(new Rect(100f, 0f, 100f, 32f), "Compile Asset");

            // GUI.Box(new Rect(0.0f, 20.0f, 600.0f, 50.0f), "Adjust zoom of middle box with slider or mouse wheel.\nMove zoom area dragging with middle mouse button or Alt+left mouse button.");
            GUI.Label(new Rect(216f, 0, 600f, 32f), $"Size: {position.size} - Mouse: {Event.current.mousePosition}");

            if (showNodeCreator) {
                newNodeWindow.Render();
            }
        }

        private void HandleEvents() {
            if (showNodeCreator) {
                newNodeWindow.Update();
            }
            // Allow adjusting the zoom with the mouse wheel as well. In this case, use the mouse coordinates
            // as the zoom center instead of the top left corner of the zoom area. This is achieved by
            // maintaining an origin that is used as offset when drawing any GUI elements in the zoom area.
            if (Event.current.type == EventType.ScrollWheel && !showNodeCreator) {
                Vector2 screenCoordsMousePos = Event.current.mousePosition;
                Vector2 delta = Event.current.delta;
                Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
                float zoomDelta = -delta.y / 150.0f;
                float oldZoom = zoom;
                zoom += zoomDelta;
                zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
                zoomCoordsOrigin += (zoomCoordsMousePos - zoomCoordsOrigin) - (oldZoom / zoom) * (zoomCoordsMousePos - zoomCoordsOrigin);
                Event.current.Use();
            }
            // Move
            if (Event.current.type == EventType.MouseDrag && ((Event.current.button == 0 && Event.current.modifiers == EventModifiers.Alt) || Event.current.button == 2)) {
                Vector2 delta = Event.current.delta;
                delta /= zoom;
                zoomCoordsOrigin -= delta;
                Event.current.Use();
            }

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A && !showNodeCreator) {
                var guid = Guid.NewGuid();
                // TODO REMAKE
                // GraphObject.Nodes.Add(new GraphFileNode(ConvertScreenCoordsToZoomCoords(Event.current.mousePosition), new Vector2(100, 100), $"Node_{guid}", guid));
                Repaint();
                Event.current.Use();
            }
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space && !showNodeCreator) {
                showNodeCreator = true;
                Repaint();
                Event.current.Use();
                newNodeWindow = new NewNodeWindow(Event.current.mousePosition, this);
            }
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape) {
                showNodeCreator = false;
                newNodeWindow = null;
                Repaint();
                Event.current.Use();
            }
            
            if (position.size != lastWindowSize) {
                lastWindowSize = position.size;
                HandleResize();
            }
        }

        private void HandleResize() {
            Debug.Log("Resized");
            zoomArea.width = lastWindowSize.x - leftPanelWidth;
            zoomArea.height = lastWindowSize.y - topPanelHeight;
        }

        private void OnGUI() {
            HandleEvents();
            DrawZoomArea();
            DrawNonZoomArea();
        }
    }
}