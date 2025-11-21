using UnityEngine;
using Metroidvania.Serialization;

namespace Metroidvania.Hotfixes.Serialization
{
    public static class AutoUserSelectPatch
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EnsureUser()
        {
            try
            {
                var dm = DataManager.instance;
                dm.Initialize();
                if (dm.selectedUserId == -1)
                    dm.ChangeSelectedUser(0);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[Hotfix] AutoUserSelectPatch falhou: {e.Message}");
            }
        }
    }
}