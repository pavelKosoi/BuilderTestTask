using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleShaker : MonoBehaviour
{
    Vector3 defaultScale;
    private void Awake()
    {
        defaultScale = transform.localScale;
    }
   
    public void Shake(float scaleFactor, float duration = 0.3f)
    {
        transform.DOPunchScale(defaultScale * scaleFactor, duration);
    }
}
