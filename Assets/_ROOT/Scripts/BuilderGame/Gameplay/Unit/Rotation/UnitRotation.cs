using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Rotation
{
    public class UnitRotation : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed;
        private Vector3 direction = Vector3.zero;
        
        public void SetRotationDirection(Vector3 direction)
        {
            this.direction = direction;
        }
        
        private void Update() => Rotate();

        private void Rotate()
        {
            if (direction.sqrMagnitude > Constants.Epsilon)
            {
                var targetRotation = Quaternion.LookRotation(direction);

                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}