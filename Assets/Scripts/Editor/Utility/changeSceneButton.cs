using UnityEngine;

namespace Metroidvania
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class changeSceneButton : MonoBehaviour
    {
        // Nome da cena que você quer carregar
        public string ThePaleMoonlight_Sample;

        // Esse método será chamado pelo botão
        public void CarregarCena()
        {
            if (!string.IsNullOrEmpty(ThePaleMoonlight_Sample))
            {
                SceneManager.LoadScene(ThePaleMoonlight_Sample);
            }
            else
            {
                Debug.LogError("Nome da cena não definido no componente BotaoTrocarCena!");
            }
        }
    }
}
