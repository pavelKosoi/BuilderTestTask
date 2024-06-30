using BuilderGame.Infrastructure.Services.Ads;
using System.Collections;
using UnityEngine;

public class Harvester : EmployeeBase
{
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    protected override async void Activate()
    {
        AdWatchResult result = await AdsManager.ShowRewardedAsync();
        if (result == AdWatchResult.Watched) StartCoroutine(Working());                        
    }

    protected override IEnumerator Working()
    {
        isActive = true;
        int cellIndex = 0;
        yield return new WaitWhile(() => gardenBed.CurrentState != GardenBed.State.Harvesting);
        while (true)
        {                      
            GardenBedCell targetCell = gardenBed.GardenBedCells[cellIndex];
            Vector3 dir = targetCell.transform.position - transform.position;
            dir.y = 0;
            unitMovement.SetMovementDirection(dir);
            if(targetCell.Culture == null) 
            {
                if (gardenBed.CurrentState == GardenBed.State.Plowing)
                {                   
                    StartCoroutine(BackToStartPos());
                    yield break;
                }
                cellIndex++;
            }
            yield return null;
        }
    }

    IEnumerator BackToStartPos()
    {
        while (true)
        {
            Vector3 dir = startPos - transform.position;
            dir.y = 0;
            unitMovement.SetMovementDirection(dir.normalized);            
            if (Vector3.Distance(transform.position, startPos) <= 0.1f)
            {
                unitMovement.SetMovementDirection(Vector3.zero);
                isActive = false;
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if(other.gameObject.layer == 6) Activate();
        }
    }

    

}
