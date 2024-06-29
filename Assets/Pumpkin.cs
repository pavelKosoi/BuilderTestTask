using DG.Tweening;
using System;
using UnityEngine;

public class Pumpkin : CultureBase
{
    public override void GrowingUp(Action onGrowedCallback)
    {
        cultureModel.transform.localScale = Vector3.zero;
        float targetTime = UnityEngine.Random.Range(growingTimeFloor, growingTimeCelling);
        cultureModel.transform.DOScale(Vector3.one, targetTime).SetEase(Ease.InOutExpo).OnComplete(()=>onGrowedCallback?.Invoke());
    }
}
