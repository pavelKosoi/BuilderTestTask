using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Pumpkin : CultureBase
{
    public override void GrowingUp(Action onGrowedCallback)
    {
        targetTime = UnityEngine.Random.Range(growingTimeFloor, growingTimeCelling);
        cultureModel.transform.localScale = Vector3.zero;
        cultureModel.transform.DOScale(Vector3.one, targetTime).SetEase(Ease.InOutExpo).OnComplete(()=>onGrowedCallback?.Invoke());
    }

    protected override void TryToHarvest()
    {
        DOTween.Sequence()
            .Append(cultureModel.transform.DOJump(cultureModel.transform.position, 1, 1, 0.5f))
            .Join(cultureModel.transform.DOScale(Vector3.one * 0.7f, 0.25f)).OnComplete(() => StartCoroutine(AttractToPlayer()));
    }
    
}
