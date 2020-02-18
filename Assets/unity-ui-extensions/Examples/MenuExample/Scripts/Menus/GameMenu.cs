using Scripts.MenuSystem;

namespace Examples.MenuExample.Scripts.Menus
{
    public class GameMenu : SimpleMenu<GameMenu>
    {
        public override void OnBackPressed()
        {
            PauseMenu.Show();
        }
    }
}