using Scripts.MenuSystem;

namespace Examples.MenuExample.Scripts.Menus
{
    public class PauseMenu : SimpleMenu<PauseMenu>
    {
        public void OnQuitPressed()
        {
            Hide();
            Destroy(this.gameObject); // This menu does not automatically destroy itself

            GameMenu.Hide();
        }
    }
}