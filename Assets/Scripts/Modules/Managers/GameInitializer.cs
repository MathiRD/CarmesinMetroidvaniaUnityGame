using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Metroidvania.Settings
{
    public class GameInitializer : MonoBehaviour
    {
        // Esse campo pode continuar no Inspector, mas não será mais usado
        [Header("Scenes")]
        [SerializeField] private AssetReference m_mainMenuSceneRef;

        private IEnumerator Start()
        {
            // Se a ref não for válida, só loga e segue a vida.
            if (!m_mainMenuSceneRef.RuntimeKeyIsValid())
            {
                Debug.LogWarning("GameInitializer: m_mainMenuSceneRef inválido. Continuando sem carregar cena via Addressables.");
            }

            // Carrega Scriptable Singletons (DataManager etc.), se existirem
            AsyncOperationHandle<IList<ScriptableObject>> scriptableSingletonsHandle =
                Addressables.LoadAssetsAsync<ScriptableObject>(
                    "Scriptable Singleton",
                    singleton =>
                    {
                        if (singleton is IInitializableSingleton initializableSingleton)
                            initializableSingleton.Initialize();

                        var type = singleton.GetType();
                        var setInstanceMethod = type.BaseType.GetMethod(
                            "SetInstance",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

                        setInstanceMethod?.Invoke(null, new object[] { singleton });
                    });

            // Carrega Persistent Singletons (InputReader, gerentes, etc.), se tiver
            AsyncOperationHandle<IList<GameObject>> persistentSingletonsHandle =
                Addressables.LoadAssetsAsync<GameObject>(
                    "Persistent Singleton",
                    go => Instantiate(go));

            // Espera terminar
            yield return persistentSingletonsHandle;
            yield return scriptableSingletonsHandle;

            // IMPORTANTE: NÃO chama mais SceneLoader aqui.
            // Nada de LoadSceneAsync, nada de LoadSceneWithoutTransition.
            // A cena que já está carregada (a da build) continua rodando.
        }
    }

    public interface IInitializableSingleton
    {
        void Initialize();
    }
}
