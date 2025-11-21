using Metroidvania.InputSystem;
using Metroidvania.SceneManagement;
using UnityEngine;

namespace Metroidvania.UI.Menus
{
    public class GamePauseMenu : CanvasMenuBase
    {
        [SerializeField] private CanvasGroup m_titleGroup;
        [SerializeField] private CanvasGroup m_mainGroup;
        [SerializeField] private OptionsMenu m_optionsMenu;

        public IMenuScreen activeScreen { get; private set; }

        private void Awake()
        {
            m_optionsMenu.OnMenuDisable += ActiveMenu;
        }

        private void OnEnable()
        {
            if (InputReader.instance == null)
            {
                Debug.LogWarning("GamePauseMenu: InputReader.instance é nulo. Desabilitando menu de pause neste build.");
                enabled = false;
                return;
            }

            // ... resto do seu código atual de OnEnable
        }

        private void OnDisable()
        {
            if (InputReader.instance == null)
                return;

            // ... resto do seu código atual de OnDisable
        }

        public void PauseGame()
        {
            GameManager.instance.PauseGame();
            menuEnabled = true;
            m_titleGroup.FadeGroup(true, Helpers.TransitionTime, SetFirstSelected);
        }

        public void ResumeGame()
        {
            m_titleGroup.FadeGroup(false, Helpers.TransitionTime, GameManager.instance.ResumeGame);
        }

        public void ActiveMenu()
        {
            menuEnabled = true;
            m_mainGroup.FadeGroup(true, Helpers.TransitionTime, SetFirstSelected);
        }

        public void OpenOptions()
        {
            SwitchToScreen(m_optionsMenu);
        }

        public void ExitGameplay()
        {
            GameManager.instance.ResumeGame();
            SceneLoader.instance.LoadMainMenu();
        }

        public void SwitchToScreen(IMenuScreen screen)
        {
            m_mainGroup.FadeGroup(false, Helpers.TransitionTime, screen.ActiveMenu);
            menuEnabled = false;
            activeScreen = screen;
        }

        public void PerformMenuClose()
        {
            if (menuEnabled)
            {
                ResumeGame();
            }
            else if (activeScreen != null)
            {
                activeScreen.DesactiveMenu();
            }
        }
    }
}
