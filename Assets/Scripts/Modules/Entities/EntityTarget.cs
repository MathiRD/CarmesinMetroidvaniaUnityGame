using UnityEngine;

namespace Metroidvania.Entities
{
    /// <summary>
    /// Marca um objeto como alvo “rastreável” pelos inimigos.
    /// </summary>
    public class EntityTarget : MonoBehaviour
    {
        /// <summary>Transform associado a este alvo (sempre tenta se auto-corrigir).</summary>
        public Transform t { get; private set; }

        /// <summary>Posição 2D segura (nunca lança NullReference; se der ruim, devolve Vector2.zero).</summary>
        public Vector2 position
        {
            get
            {
                // Garante que t esteja sempre setado
                if (t == null)
                    t = transform;

                if (t == null)
                    return Vector2.zero;

                return (Vector2)t.position;
            }
        }

        private void Awake()
        {
            // Cache inicial
            t = transform;
        }

        private void OnEnable()
        {
            if (t == null)
                t = transform;

            // Registra no EntitiesManager se existir (editor ou build)
            var mgr = EntitiesManager.instance;
            if (mgr != null)
            {
                mgr.AddTarget(this);
            }
        }

        private void OnDisable()
        {
            var mgr = EntitiesManager.instance;
            if (mgr != null)
            {
                mgr.RemoveTarget(this);
            }
        }
    }
}
