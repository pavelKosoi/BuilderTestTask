using UnityEngine;

namespace BuilderGame.Gameplay.Unit
{
    [RequireComponent(typeof(CharacterController))]
    public class UnitMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed;
        
        [SerializeField] 
        private float smoothness = 0.1f;
        
        [Header("References")]
        [SerializeField]
        private CharacterController characterController;

        public Vector3 Direction { get; private set; } = Vector3.zero;
        public Vector3 TargetVelocity { get; private set; }
        
        private Vector3 smoothedVelocity;
        private Vector3 currentVelocity;

        private void OnValidate()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void SetMovementDirection(Vector3 direction)
        {
            Direction = direction;
        }
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            TargetVelocity = Vector3.zero;

            if (Direction.sqrMagnitude > Constants.Epsilon)
            {
                TargetVelocity = Direction * Speed;
            }
            
            smoothedVelocity =
                    Vector3.SmoothDamp(smoothedVelocity, TargetVelocity + Physics.gravity, 
                        ref currentVelocity, smoothness);
            
            characterController.Move(smoothedVelocity * Time.deltaTime);
        }
    }
}