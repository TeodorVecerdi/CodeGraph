using Scripts.MenuSystem;
using UnityEngine;

namespace Examples.MenuExample.Scripts.Menus
{
    public class MainMenu : SimpleMenu<MainMenu>
    {
        public void OnPlayPressed()
        {
            GameMenu.Show();
        }

        public void OnOptionsPressed()
        {
            OptionsMenu.Show();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}