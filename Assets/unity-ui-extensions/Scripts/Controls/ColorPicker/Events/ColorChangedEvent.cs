using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Controls.ColorPicker.Events {
    [Serializable]
    public class ColorChangedEvent : UnityEvent<Color>
    {

    }
}