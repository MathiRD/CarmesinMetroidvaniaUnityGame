#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Metroidvania.Abilities;

namespace Metroidvania.Hotfixes.Editor
{
    public static class MetroidvaniaTools
    {
        [MenuItem("Tools/Metroidvania/Reset Abilities (mv_has_dash)")]
        public static void ResetAbilities()
        {
            PlayerAbilities.ResetAll();
            Debug.Log("[Tools] Abilities resetadas (mv_has_dash limpo).");
        }
    }
}
#endif