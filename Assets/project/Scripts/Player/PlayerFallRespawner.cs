using UnityEngine;

namespace ArenaSurvival.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerFallRespawner : MonoBehaviour
    {
        [Header("Respawn Settings")]
        [SerializeField]
        private float fallThresholdY = -10f;

        [SerializeField, Min(0f)]
        private float safeHeightOffset = 0.2f;

        private CharacterController characterController;
        private Vector3 respawnPosition;
        private Quaternion respawnRotation;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            SaveCurrentTransformAsRespawnPoint();
        }

        private void Update()
        {
            if (transform.position.y < fallThresholdY)
            {
                Respawn();
            }
        }

        [ContextMenu("Save Current Transform As Respawn Point")]
        public void SaveCurrentTransformAsRespawnPoint()
        {
            respawnPosition = transform.position;
            respawnRotation = transform.rotation;
        }

        [ContextMenu("Respawn")]
        public void Respawn()
        {
            characterController.enabled = false;
            transform.SetPositionAndRotation(
                respawnPosition + Vector3.up * safeHeightOffset,
                respawnRotation);
            characterController.enabled = true;
        }
    }
}

