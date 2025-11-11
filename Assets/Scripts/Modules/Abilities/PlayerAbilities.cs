using UnityEngine;

namespace Metroidvania.Abilities
{
    /// <summary>
    /// Minimal ability flags with PlayerPrefs persistence (simple & robust).
    /// </summary>
    public static class PlayerAbilities
    {
        private const string KEY_HAS_DASH = "mv_has_dash";

        public static bool HasDash
        {
            get => PlayerPrefs.GetInt(KEY_HAS_DASH, 0) == 1;
            set { PlayerPrefs.SetInt(KEY_HAS_DASH, value ? 1 : 0); PlayerPrefs.Save(); }
        }

        /// <summary>Reset all abilities (useful for testing).</summary>
        public static void ResetAll()
        {
            PlayerPrefs.DeleteKey(KEY_HAS_DASH);
        }
    }
}
