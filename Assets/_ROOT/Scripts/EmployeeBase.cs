using BuilderGame.Gameplay.Unit;
using BuilderGame.Infrastructure.Services.Ads;
using BuilderGame.Infrastructure.Services.Ads.Fake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EmployeeBase : MonoBehaviour
{
    [SerializeField] protected GardenBed gardenBed;
    protected UnitMovement unitMovement;
    protected bool isActive;

    protected abstract void Activate();
    protected abstract IEnumerator Working();

    private void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
    }

}
