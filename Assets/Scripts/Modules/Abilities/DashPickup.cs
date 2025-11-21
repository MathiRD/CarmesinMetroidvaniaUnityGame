using UnityEngine;
using Metroidvania.Abilities;
using Metroidvania.Characters.Knight;

namespace Metroidvania.Abilities
{
    /// <summary>
    /// Place this on a trigger collider in your scene to unlock DASH when the player touches it.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DashPickup : MonoBehaviour
    {
        [Tooltip("Optional VFX or object to enable when collected.")]
        public GameObject collectEffect;
        [Tooltip("Auto destroy the pickup after collection.")]
        public bool destroyOnCollect = true;

        private void Reset()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Accept either the Knight controller or anything tagged Player
            var isPlayer = other.GetComponentInParent<KnightCharacterController>() != null || other.CompareTag("Player");
            if (!isPlayer) return;

            if (!PlayerAbilities.HasDash)
            {
                PlayerAbilities.HasDash = true;
                Debug.Log("Dash acquired! Press your Dash key/button to use it.");
            }

            if (collectEffect) collectEffect.SetActive(true);
            if (destroyOnCollect) Destroy(gameObject);
        }
    }
}
