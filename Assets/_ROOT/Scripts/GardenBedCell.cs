using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBedCell : MonoBehaviour
{
    public GardenBed.State currentState;
    [SerializeField] int plowTimes;
    [SerializeField] ParticleEmiter plowingFx;
    [SerializeField] ParticleEmiter dirtFx;
    [SerializeField] MeshRenderer groundMesh;
    [SerializeField] Material groundMaterial;    
    GardenBed GardenBedBase;
    CultureBase culture;

    private void Awake()
    {
        GardenBedBase = GetComponentInParent<GardenBed>();
    }

    public void Work()
    {
        switch (currentState) 
        {
            case GardenBed.State.Plowing:
                plowTimes -= 1;
                plowingFx.Emit();
                if (plowTimes <= 0) SetPlowed();
                break;
            case GardenBed.State.Seeding:
                if(!culture) SetCulture();
                break;
        }
    }

    void SetCulture()
    {
        culture = Instantiate(GardenBedBase.CulturePrefab, transform).GetComponent<CultureBase>();
        culture.GrowingUp(OnCultureGrowed);
    }

    public void OnCultureGrowed()
    {
        currentState = GardenBed.State.Harvesting;
        GardenBedBase.TryToNextState(currentState);
    }

    void SetPlowed()
    {
        dirtFx.Emit();
        groundMesh.material = groundMaterial;
        currentState = GardenBed.State.Seeding;
        GardenBedBase.TryToNextState(currentState);
    }
}
