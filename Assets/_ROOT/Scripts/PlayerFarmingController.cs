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
    public static PlayerFarmingController Instance;

    [Serializable]
    public struct Doing
    {
        public GardenBed.State doingType;
        public bool breakOnMove;
        public GameObject instrument;
        public ParticleEmiter doingFx;
    }

    [SerializeField] Doing[] doings;

    UnitMovementAnimation movementAnimation;
    UnitMovement unitMovement;
    Coroutine CheckCellsRoutine;
    AngleAndRadiusHandler angleAndRadiusHandler;
    GardenBed.State currentState = GardenBed.State.Nothifng;    



    private void Awake()
    {
        Instance = this;
        movementAnimation = GetComponentInChildren<UnitMovementAnimation>();
        angleAndRadiusHandler = GetComponent<AngleAndRadiusHandler>();
        unitMovement = GetComponent<UnitMovement>();
    }

    public void StartDoingByState(GardenBed.State gardebBedState)
    {
        CheckCellsRoutine = StartCoroutine(CheckGardenBedCells());        
        foreach (var item in doings)
        {
            item.instrument.SetActive(item.doingType == gardebBedState);
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
                List<Collider> colliders = GetCellsInAngle();
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

    IEnumerator StartWorkingByState(GardenBed.State state)
    {
        movementAnimation.Animator.SetBool($"{state}Work", true);
        while (true)
        {
            bool outOfWorking = false;
            if (unitMovement.TargetVelocity.magnitude != 0)
            {
                foreach (var item in doings)
                {
                    if (item.doingType == currentState && item.breakOnMove)
                    {
                        outOfWorking = true;
                    }
                }
            }
            else
            {
                List<Collider> colliders = GetCellsInAngle();
                bool allCellsWorked = true;
                foreach (var item in colliders)
                {
                    GardenBedCell gardenBedCell = item.GetComponentInParent<GardenBedCell>();
                    if (gardenBedCell.currentState == currentState)
                    {
                        allCellsWorked = false;
                    }
                }
               outOfWorking = allCellsWorked;
            }
            if (outOfWorking)
            {
                movementAnimation.Animator.SetBool($"{state}Work", false);
                CheckCellsRoutine = StartCoroutine(CheckGardenBedCells());
                yield break;
            }

            yield return null;
        }
    }

    List<Collider> GetCellsInAngle()
    {
        List<Collider> colliders = Physics.OverlapSphere(transform.position, angleAndRadiusHandler.Radius, LayerMask.GetMask("GardenBedCell")).ToList();
        RemoveCollidersNotInAngle(colliders, angleAndRadiusHandler.Angle);
        return colliders;
    }

    Doing GetDoingByState(GardenBed.State state)
    {
        foreach (var item in doings)
        {
            if (item.doingType == state) return item;
        }
        return new Doing();
    }

    public void Work()
    {
        Doing currentDoing = GetDoingByState(currentState);
        if (currentDoing.doingFx) currentDoing.doingFx.Emit();

        List<Collider> colliders = GetCellsInAngle();

        foreach (var item in colliders)
        {
            GardenBedCell gardenBedCell = item.GetComponentInParent<GardenBedCell>();
            if (gardenBedCell.currentState == currentState)
            {
                gardenBedCell.Work();
            }
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
        foreach (var item in doings)
        {
            item.instrument.SetActive(false);
        }
        movementAnimation.Animator.SetTrigger("BackToDefault");
    }
}
