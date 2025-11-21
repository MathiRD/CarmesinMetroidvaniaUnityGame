
using UnityEngine;

namespace Metroidvania.Hotfixes.Ability
{
    /// <summary>
    /// Coloque este script no objeto de pickup do Dash (colisor como trigger).
    /// Ele desbloqueia o dash, salva em PlayerPrefs e se auto-destroi.
    /// Se o dash já estiver desbloqueado, se auto-destroi na Awake.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DashUnlockPickup : MonoBehaviour
    {
        private const string KEY = "ability_dash";

        void Awake()
        {
            if (PlayerPrefs.GetInt(KEY, 0) == 1)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            ForceDashLocked.UnlockDash(other.gameObject);
            try
            {
                // Tenta tocar um SFX se houver um AudioSource
                var src = GetComponent<AudioSource>();
                if (src != null) src.Play();
            } catch {}

            Destroy(gameObject);
        }
    }
}
