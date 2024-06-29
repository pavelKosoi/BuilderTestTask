using BuilderGame.Gameplay.Unit.Animation;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmingController : MonoBehaviour
{
    [SerializeField] Transform instruments;
    UnitMovementAnimation movementAnimation;

    private void Awake()
    {
        movementAnimation = GetComponentInChildren<UnitMovementAnimation>();
    }

    public void StartDoingByState(GardenBedBase.State gardebBedState)
    {
        for (int i = 0; i < instruments.childCount; i++)
        {
            instruments.GetChild(i).gameObject.SetActive(i == (int)gardebBedState);
        }

        int targetLayer = (int)gardebBedState + 1;
        movementAnimation.SwitchAnimLayers(targetLayer);
    }  

    public void BackToDefault()
    {
        for (int i = 0; i < instruments.childCount; i++)
        {
            instruments.GetChild(i).gameObject.SetActive(false);
        }
        movementAnimation.SwitchAnimLayers(0);
    }
}
