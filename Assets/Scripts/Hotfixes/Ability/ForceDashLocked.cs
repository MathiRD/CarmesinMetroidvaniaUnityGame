
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Metroidvania.Hotfixes.Ability
{
    /// <summary>
    /// Força o dash a iniciar bloqueado usando PlayerPrefs("ability_dash").
    /// Anexe este script ao Player root (prefab/instância).
    /// Ele desabilita quaisquer componentes que tenham "dash" no nome do tipo,
    /// e tenta setar campos bool comuns (dashUnlocked/canDash/isDashEnabled) para false.
    /// Quando desbloqueado em runtime, chame ForceDashLocked.UnlockDash(gameObject).
    /// </summary>
    public class ForceDashLocked : MonoBehaviour
    {
        private const string KEY = "ability_dash";
        private MonoBehaviour[] _dashBehaviours;

        void Awake()
        {
            _dashBehaviours = GetComponentsInChildren<MonoBehaviour>(true)
                .Where(m => m != null && m.GetType().Name.ToLower().Contains("dash"))
                .ToArray();

            if (PlayerPrefs.GetInt(KEY, 0) == 0)
            {
                ApplyLock(true);
            }
            else
            {
                ApplyLock(false);
            }
        }

        private void ApplyLock(bool locked)
        {
            foreach (var mb in _dashBehaviours)
            {
                if (mb == null) continue;
                var t = mb.GetType();

                // Desabilita o componente por padrão
                try { mb.enabled = !locked; } catch {}

                // Tenta setar campos comuns (reflection)
                foreach (var fname in new[] { "dashUnlocked", "canDash", "isDashEnabled", "IsDashUnlocked", "DashUnlocked" })
                {
                    var f = t.GetField(fname, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (f != null && f.FieldType == typeof(bool))
                    {
                        f.SetValue(mb, !locked);
                    }
                }

                // Tenta chamar um método comum SetDashEnabled(bool)
                var mSet = t.GetMethod("SetDashEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (mSet != null)
                {
                    try { mSet.Invoke(mb, new object[] { !locked }); } catch {}
                }
            }
        }

        public static void UnlockDash(GameObject player)
        {
            PlayerPrefs.SetInt(KEY, 1);
            PlayerPrefs.Save();
            var gate = player.GetComponentInChildren<ForceDashLocked>(true);
            if (gate != null) gate.ApplyLock(false);
        }
    }
}
