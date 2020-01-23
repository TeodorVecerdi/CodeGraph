using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class NodeMB : MonoBehaviour, IDragHandler {
    public GameObject Inputs;
    public GameObject Outputs;
    public ValidNodeType NodeType;
    private Node node;

    public void Start() {
        GenerateNode();
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
        int a = 10;
    }

    private void GenerateNode() {
        string nodeClassName = NodeType + "Node";
        // node = (Node)Activator.CreateInstance(Type.GetType(nodeClassName));
        // Debug.Log(node.GetType());
    }
}

public enum ValidNodeType {
    Vector2,
    Print
}