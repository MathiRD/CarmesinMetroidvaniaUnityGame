#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Metroidvania.Hotfixes.Editor
{
    public static class ClearPrefsMenu
    {
        [MenuItem("Tools/Metroidvania/Clear All PlayerPrefs")]
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("[Tools] PlayerPrefs limpos.");
        }
    }
}
#endif