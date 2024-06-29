using BuilderGame.Gameplay.Unit;
using BuilderGame.Gameplay.Unit.Animation;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerFarmingController : MonoBehaviour
{
    [Serializable]
    public struct Doing
    {
        public GardenBedBase.State doingType;
        public bool breakOnMove;
    }

    [SerializeField] Transform instruments;
    [SerializeField] Doing[] doings;

    UnitMovementAnimation movementAnimation;
    UnitMovement unitMovement;
    Coroutine CheckCellsRoutine;
    AngleAndRadiusHandler angleAndRadiusHandler;
    GardenBedBase.State currentState = GardenBedBase.State.Nothifng;    



    private void Awake()
    {
        movementAnimation = GetComponentInChildren<UnitMovementAnimation>();
        angleAndRadiusHandler = GetComponent<AngleAndRadiusHandler>();
        unitMovement = GetComponent<UnitMovement>();
    }

    public void StartDoingByState(GardenBedBase.State gardebBedState)
    {
        CheckCellsRoutine = StartCoroutine(CheckGardenBedCells());
        for (int i = 0; i < instruments.childCount; i++)
        {
            instruments.GetChild(i).gameObject.SetActive(i == (int)gardebBedState);
        }
        currentState = gardebBedState;
        movementAnimation.Animator.SetTrigger(gardebBedState.ToString());
    }
   
    IEnumerator CheckGardenBedCells()
    {
        while (true)
        {
            if (unitMovement.TargetVelocity.magnitude == 0)
            {
                List<Collider> colliders = Physics.OverlapSphere(transform.position, angleAndRadiusHandler.Radius, LayerMask.GetMask("GardenBedCell")).ToList();
                RemoveCollidersNotInAngle(colliders, angleAndRadiusHandler.Angle);

                foreach (var item in colliders)
                {
                    GardenBedCell gardenBedCell = item.GetComponentInParent<GardenBedCell>();
                    if (gardenBedCell.currentState == currentState)
                    {
                        StartCoroutine(StartWorkingByState(currentState));
                        yield break;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StartWorkingByState(GardenBedBase.State state)
    {
        movementAnimation.Animator.SetBool($"{state}Work", true);
        while (true)
        {
            if (unitMovement.TargetVelocity.magnitude != 0)
            {
                foreach (var item in doings)
                {
                    if (item.doingType == currentState && item.breakOnMove)
                    {
                        movementAnimation.Animator.SetBool($"{state}Work", false);
                        CheckCellsRoutine = StartCoroutine(CheckGardenBedCells());
                        yield break;
                    }
                }
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    void RemoveCollidersNotInAngle(List<Collider> colliders, float targetAngle)
    {
        List<Collider> toRemove = new List<Collider>();
        foreach (var item in colliders)
        {
            Vector3 dir = item.transform.position - transform.position;
            dir.y = 0;
            float angle = Vector3.Angle(dir, transform.forward);
            if (angle > targetAngle) toRemove.Add(item);
        }
        colliders.RemoveAll(collider => toRemove.Contains(collider));
    }

    public void BackToDefault()
    {
        if (CheckCellsRoutine != null) StopCoroutine(CheckCellsRoutine); 
        for (int i = 0; i < instruments.childCount; i++)
        {
            instruments.GetChild(i).gameObject.SetActive(false);
        }
        movementAnimation.Animator.SetTrigger("BackToDefault");
    }
}
