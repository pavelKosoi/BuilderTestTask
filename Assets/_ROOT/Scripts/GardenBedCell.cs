using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBedCell : MonoBehaviour
{
    public GardenBedBase.State currentState;
    [SerializeField] int plowTimes;
    [SerializeField] ParticleEmiter plowingFx;
    [SerializeField] ParticleEmiter dirtFx;
    [SerializeField] MeshRenderer groundMesh;
    [SerializeField] Material groundMaterial;
    GardenBedBase GardenBedBase;

    private void Awake()
    {
        GardenBedBase = GetComponentInParent<GardenBedBase>();
    }

    public void Work()
    {
        switch (currentState) 
        {
            case GardenBedBase.State.Plowing:
                plowTimes -= 1;
                plowingFx.Emit();
                if (plowTimes <= 0) SetPlowed();
                break;
        }
    }

    void SetPlowed()
    {
        dirtFx.Emit();
        groundMesh.material = groundMaterial;
        currentState = GardenBedBase.State.Seeding;
        GardenBedBase.TryToNextState(currentState);
    }
}
