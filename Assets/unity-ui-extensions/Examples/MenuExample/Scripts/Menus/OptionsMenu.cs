using Scripts.MenuSystem;
using UnityEngine.UI;

namespace Examples.MenuExample.Scripts.Menus
{
    public class OptionsMenu : SimpleMenu<OptionsMenu>
    {
        public Slider Slider;

        public void OnMagicButtonPressed()
        {
            AwesomeMenu.Show(Slider.value);
        }
    }
}