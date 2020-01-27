using System;
using CodeGraph;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Math = System.Math;

public class TestWindow : EditorWindow {
    public Vector2 DragOffset = new Vector2(-50000f, -50000f);
    private Rect window1;
    private Rect window2;
    private Connection connection;
    private Box box1 = new Box(new Vector2(50900, 50000), new Vector2(100, 100), "Node 1");
    private Box box2 = new Box(new Vector2(50600, 50300), new Vector2(100, 100), "Node 2");
    private float zoom = 1f;
    private float zoomSpeed = -0.01F;
    private bool startedConnection;
    private bool finishedConnection;
    private Vector2 vanishingPoint = new Vector2(0, 21);

    [MenuItem("Window/Code Graph")]
    public static void ShowExample() {
        var window = GetWindow<TestWindow>("Code Graph");
        window.wantsMouseMove = true;

        // TestWindow wnd = GetWindow<TestWindow>();
        // wnd.titleContent = new GUIContent("Code Graph");
    }

    private void OnGUI() {
        // Check out this http://martinecker.com/martincodes/unity-editor-window-zooming/
        // GUI.EndGroup();
        bool didPan = false;

        // GUI.Label(new Rect(0, 40 + 0, 1000, 20), $"offset: {DragOffset}");
        // GUI.Label(new Rect(0, 40 + 20, 1000, 20), $"box1 Pos: {box1.Position}");
        // GUI.Label(new Rect(0, 40 + 40, 1000, 20), $"box2 Pos: {box2.Position}");
        // GUI.Label(new Rect(0, 40 + 60, 1000, 20), $"mouse Pos: {Event.current.mousePosition}");

        // GUI.Label(new Rect(0, 40 + 60, 1000, 20), $"line start: {connection.Start}");
        // GUI.Label(new Rect(0, 40 + 80, 1000, 20), $"line end: {connection.End}");
        GUI.Label(new Rect(0, 40 + 0, 1000, 20), $"zoom: {zoom}");
        // vanishingPoint = EditorGUILayout.Vector2Field("vanishing point", vanishingPoint);

        if (Event.current.type == EventType.ScrollWheel) {
            float scroll = Event.current.delta.y;
            if (scroll < 0f) scroll = -1;
            else if (scroll > 0f) scroll = 1;
            Debug.Log($"Scroll: {scroll}");
            zoom += scroll * zoomSpeed;
            zoom = Mathf.Clamp(zoom, 0.2f, 5f);

            var mousePosition = Event.current.mousePosition;

            // vanishingPoint.x = position.height / 2f + mousePosition.x * zoom;
            // vanishingPoint.y = position.width / 2f + mousePosition.y * zoom;
            Repaint();
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space) {
            if (!startedConnection && Event.current.mousePosition.x >= box1.Position.x + DragOffset.x && Event.current.mousePosition.x <= box1.Position.x + DragOffset.x + box1.Size.x && Event.current.mousePosition.y >= box1.Position.y + DragOffset.y && Event.current.mousePosition.y <= box1.Position.y + DragOffset.y + box1.Size.y) {
                startedConnection = true;
                connection = new Connection(box1.Position, Event.current.mousePosition);
            } else if (startedConnection && Event.current.mousePosition.x >= box2.Position.x + DragOffset.x && Event.current.mousePosition.x <= box2.Position.x + DragOffset.x + box2.Size.x && Event.current.mousePosition.y >= box2.Position.y + DragOffset.y && Event.current.mousePosition.y <= box2.Position.y + DragOffset.y + box2.Size.y) {
                finishedConnection = true;
                startedConnection = false;
            }
        }

        box1.Update(this);
        box2.Update(this);
        if (startedConnection) {
            var pos = box1.Position;
            pos.x += box1.Size.x;
            connection.Update(pos, Event.current.mousePosition - DragOffset, this);
        } else if (finishedConnection) {
            connection.Update(box1, box2, this);
        }

        if (Event.current.type == EventType.MouseDrag) {
            if (Event.current.button == 1) {
                DragOffset += Event.current.delta;
                didPan = true;
            }
        }

        // GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        // myString = EditorGUILayout.TextField("Text Field", myString);

        // groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        // myBool = EditorGUILayout.Toggle("Toggle", myBool);
        // myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        // EditorGUILayout.EndToggleGroup();

        // boxWidth = EditorGUILayout.Slider("Width", boxWidth, 25, 300);
        // boxHeight = EditorGUILayout.Slider("Height", boxHeight, 25, 300);
        Matrix4x4 oldMatrix = GUI.matrix;

        //Scale my gui matrix
        Matrix4x4 Translation = Matrix4x4.TRS(vanishingPoint, Quaternion.identity, Vector3.one);
        Matrix4x4 Scale = Matrix4x4.Scale(new Vector3(zoom, zoom, 1.0f));
        GUI.matrix = Translation * Scale * Translation.inverse;

        GUI.BeginGroup(new Rect(DragOffset.x, DragOffset.y, 100000, 100000));

        connection?.Render();
        box1.Render();
        box2.Render();

        // GUI.matrix = oldMatrix;

        BeginWindows();
        window1 = GUI.Window(2, window1, DrawNodeWindow, "Node A"); // Updates the Rect's when these are dragged
        window2 = GUI.Window(3, window2, DrawNodeWindow, "Node B");
        EndWindows();

        GUI.matrix = oldMatrix;
        GUI.EndGroup();
        if (didPan) {
            Repaint();
        }
    }

    void DrawNodeWindow(int id) {
        GUI.DragWindow();
    }

    public void OnEnable() {
        return;

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/TestWindow.uss");

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/TestWindow.uxml");

        VisualElement uxml = visualTree.CloneTree();
        uxml.styleSheets.Add(styleSheet);
        root.Add(uxml);

        // VisualElement labelWithStyle = new Label("Hello World! With Style");
        // labelWithStyle.styleSheets.Add(styleSheet);
        // root.Add(labelWithStyle);
    }

    internal class Box {
        public static bool IsAnyDragged;
        public Vector2 Position;
        public Vector2 Size;
        public String Title;
        public bool StartedDragging;

        public Box(Vector2 position, Vector2 size, string title) {
            Position = position;
            Size = size;
            Title = title;
        }

        public void Update(TestWindow window) {
            if (Event.current.type == EventType.MouseUp && StartedDragging) {
                StartedDragging = false;
                IsAnyDragged = false;
            } else if (Event.current.type == EventType.MouseDrag) {
                if (Event.current.button != 0)
                    return;
                if (StartedDragging) {
                    Position += Event.current.delta;
                    window.Repaint();
                } else if (!IsAnyDragged) {
                    var mouseLocation = Event.current.mousePosition;
                    if (mouseLocation.x >= Position.x + window.DragOffset.x && // right of the left edge AND
                        mouseLocation.x <= Position.x + window.DragOffset.x + Size.x && // left of the right edge AND
                        mouseLocation.y >= Position.y + window.DragOffset.y && // below the top AND
                        mouseLocation.y <= Position.y + window.DragOffset.y + Size.y) {
                        // above the bottom
                        StartedDragging = true;
                        IsAnyDragged = true;
                    }
                }
            }
        }

        public void Render() {
            GUI.Box(new Rect(Position.x, Position.y, Size.x, Size.y), Title);
        }
    }

    internal class Connection {
        public static Vector2 Offset = new Vector2(100, 0);
        public Vector2 Start;
        public Vector2 End;

        public Connection(Vector2 start, Vector2 end) {
            Start = start;
            End = end;
        }

        public void Render() {
            var distance = End.x - Start.x;
            var endTangent = End - Offset;
            if (distance <= 0 && distance >= -Offset.x) {
                endTangent = End - new Vector2(distance.Map(-Offset.x, 0, -Offset.x, Offset.x), 0);
            } else if (distance < -Offset.x) {
                endTangent = End + Offset;
            }

            Handles.DrawBezier(Start, End, Start + Offset, endTangent, Color.yellow, Texture2D.whiteTexture, 2);
        }

        public void Update(Box start, Box end, TestWindow window) {
            var newStartPosition = start.Position;
            newStartPosition.x += start.Size.x;
            var newEndPosition = end.Position;
            if (End != newEndPosition) {
                End = newEndPosition;
                window.Repaint();
            } else if (Start != newStartPosition) {
                Start = newStartPosition;
                window.Repaint();
            }
        }

        public void Update(Vector2 start, Vector2 end, TestWindow window) {
            if (End != end) {
                End = end;
                window.Repaint();
            } else if (Start != start) {
                Start = start;
                window.Repaint();
            }
        }
    }
}