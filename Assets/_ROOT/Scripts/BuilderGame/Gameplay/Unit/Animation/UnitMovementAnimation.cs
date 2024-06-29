using DG.Tweening;
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
        int currentLayer;


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


        public void SwitchAnimLayers(int targetLayer)
        {

            DOTween.Sequence()
            .Append(DOTween.To(() => 1, x => animator.SetLayerWeight(currentLayer, x), 0, 0.5f)).SetEase(Ease.Linear)
            .Join(DOTween.To(() => 0, x => animator.SetLayerWeight(targetLayer, x), 1, 0.5f)).SetEase(Ease.Linear).OnComplete(() => currentLayer = targetLayer);
        }

    }
}