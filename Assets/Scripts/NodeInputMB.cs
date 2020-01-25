using TMPro;
public class NodeInputMB : UnityEngine.MonoBehaviour {
    public InputNode InputNode;
    public UnityEngine.GameObject KnobSelected;
    public TMP_Text Label;
    public void SetLabel(string labelText) {
        Label.text = labelText;
    }

    public void SetSelected(bool selected) {
        KnobSelected.SetActive(selected);
    }
}