using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CultureBase : MonoBehaviour
{
    [SerializeField] protected float growingTimeCelling;
    [SerializeField] protected float growingTimeFloor;
    [SerializeField] protected GameObject cultureModel;
    public abstract void GrowingUp(Action onGrowedCallback);
}
