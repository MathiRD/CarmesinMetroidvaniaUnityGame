using UnityEngine;
using Unity.Cinemachine;
using Metroidvania.Characters.Knight;

namespace Metroidvania.Utility
{
    /// <summary>
    /// Drop this on the CinemachineCamera object (child of Main Camera).
    /// It will auto-assign the player as Follow target and set a reasonable orthographic size.
    /// </summary>
    [RequireComponent(typeof(CinemachineCamera))]
    public class AutoAssignCameraFollow : MonoBehaviour
    {
        [Min(1f)]
        public float orthographicSize = 6f;

        private void Awake()
        {
            var vcam = GetComponent<CinemachineCamera>();
            if (!vcam) return;

            // Try find the player automatically
            var player = FindObjectOfType<KnightCharacterController>();
            if (player != null)
            {
                vcam.Follow = player.transform;
            }

            // Ensure ortho size (Cinemachine 3.x API)
            var lens = vcam.Lens;
            lens.OrthographicSize = orthographicSize;
            vcam.Lens = lens;

            // Optional: add a composer for basic damping
            var composer = vcam.GetComponent<CinemachinePositionComposer>();
            if (!composer)
                composer = vcam.gameObject.AddComponent<CinemachinePositionComposer>();
            composer.Damping = new Vector3(0.3f, 0.3f, 0f);
        }
    }
}
