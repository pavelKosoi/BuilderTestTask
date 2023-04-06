using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Rotation
{
    [RequireComponent(typeof(UnitRotation), typeof(UnitMovement))]
    public class MovementControlRotation : MonoBehaviour
    {
        [SerializeField]
        private UnitRotation unitRotation;
        [SerializeField]
        private UnitMovement unitMovement;
        private void OnValidate()
        {
            unitRotation = GetComponent<UnitRotation>();
            unitMovement = GetComponent<UnitMovement>();
        }
        
        private void Update()
        {
            if (unitMovement.TargetVelocity.sqrMagnitude > Constants.Epsilon)
            {
                unitRotation.SetRotationDirection(unitMovement.Direction);
            }
        }
    }
}