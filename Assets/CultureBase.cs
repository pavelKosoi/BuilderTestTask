using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CultureBase : MonoBehaviour
{
    [SerializeField] protected float growingTimeCelling;
    [SerializeField] protected float growingTimeFloor;
    [SerializeField] protected GameObject cultureModel;
    [SerializeField] protected float playerAttractDuration;
    protected float targetTime;
    protected bool harvested;
    protected bool ripe;

    public GardenBedCell gardenBedCell {  get; set; }
    public float TargetTime { get { return targetTime; } }
    public abstract void GrowingUp(Action onGrowedCallback);
    protected abstract void TryToHarvest();
    protected virtual IEnumerator AttractToPlayer()
    {
        float duration = playerAttractDuration;
        float elapsedTime = 0f;
        Vector3 startPoint = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            Vector3 endPoint = PlayerFarmingController.Instance.transform.position;
            Vector3 controlPoint = (startPoint + endPoint) / 2 + Vector3.up * 5;
            Vector3 position = CalculateQuadraticBezierPoint(t, startPoint, controlPoint, endPoint);

            transform.position = position;

            yield return null;
        }

        transform.position = PlayerFarmingController.Instance.transform.position;
        PlayerFarmingController.Instance.scaleShaker.Shake(0.15f);
        Destroy(gameObject);
    }
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1; 
        p += tt * p2;
        return p;
    }
    
}
