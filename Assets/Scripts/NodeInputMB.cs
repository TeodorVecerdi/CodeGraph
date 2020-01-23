using TMPro;
using UnityEngine;

public class NodeInputMB : MonoBehaviour {
    public InputNode InputNode;
    public GameObject KnobSelected;
    public TMP_Text Label;
    public void SetLabel(string labelText) {
        Label.text = labelText;
    }

    public void SetSelected(bool selected) {
        KnobSelected.SetActive(selected);
    }
}