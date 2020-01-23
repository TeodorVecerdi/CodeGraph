using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class TestWindow : EditorWindow
{
    // [MenuItem("Window/Code Graph")]
    public static void ShowExample()
    {
        TestWindow wnd = GetWindow<TestWindow>();
        wnd.titleContent = new GUIContent("Code Graph");
    }

    public void OnEnable()
    {
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
}