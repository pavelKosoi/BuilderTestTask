using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitMovementAnimation : MonoBehaviour
    {
        [SerializeField] 
        private float damp = 0.15f;
        
        [SerializeField]
        private UnitMovement unitMovement;
        [SerializeField]
        private Animator animator;

        private readonly int movementParameter = Animator.StringToHash("Movement");

        private void OnValidate()
        {
            unitMovement = GetComponentInParent<UnitMovement>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            var velocityMagnitude = unitMovement.Direction.normalized.sqrMagnitude;

            animator.SetFloat(movementParameter, velocityMagnitude, damp, Time.deltaTime);
        }
    }
}