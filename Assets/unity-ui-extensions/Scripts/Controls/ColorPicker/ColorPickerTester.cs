///Credit judah4
///Sourced from - http://forum.unity3d.com/threads/color-picker.267043/

using UnityEngine;

namespace Scripts.Controls.ColorPicker
{
    public class ColorPickerTester : MonoBehaviour
    {
        public Renderer pickerRenderer;
        public ColorPickerControl picker;

        void Awake()
        {
            pickerRenderer = GetComponent<Renderer>();
        }
        // Use this for initialization
        void Start()
        {
            picker.onValueChanged.AddListener(color =>
            {
                pickerRenderer.material.color = color;
            });
        }
    }
}