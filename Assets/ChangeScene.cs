using UnityEngine;
using UnityEngine.SceneManagement;

namespace Metroidvania
{
    public class ChangeScene : MonoBehaviour
    {
        // Nome da cena que você quer carregar ao clicar no botão
        [SerializeField] private string sceneToLoad;

        public void RestartGame()
        {
            // Garante que o tempo volte ao normal caso tenha sido pausado
            Time.timeScale = 1f;

            // Carrega a cena escolhida
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
