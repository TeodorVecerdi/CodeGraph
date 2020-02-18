using UnityEngine;
using UnityEngine.UI;

namespace Examples.RadialSlider
{
    public class UpdateRadialValue : MonoBehaviour
    {
        public InputField input;
        public Scripts.Controls.RadialSlider slider;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public void UpdateSliderValue()
        {
            float value;
            float.TryParse(input.text, out value);
            slider.Value = value;
        }

        public void UpdateSliderAndle()
        {
            int value;
            int.TryParse(input.text, out value);
            slider.Angle = value;
        }
    }
}