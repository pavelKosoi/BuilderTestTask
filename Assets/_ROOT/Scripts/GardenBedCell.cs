using DG.Tweening;
using UnityEngine;

public class GardenBedCell : MonoBehaviour
{
    public GardenBed.State currentState;
    [SerializeField] int plowTimesMax;
    [SerializeField] ParticleEmiter plowingFx;
    [SerializeField] ParticleEmiter dirtFx;
    [SerializeField] MeshRenderer groundMesh;
    [SerializeField] Material groundMaterial;
    [SerializeField] Material darkGrassMaterial;
    [SerializeField] Transform sprout;
    GardenBed GardenBed;
    CultureBase culture;
    int plowTimes;

    public CultureBase Culture { get { return culture; } }


    private void Awake()
    {
        GardenBed = GetComponentInParent<GardenBed>();
        plowTimes = plowTimesMax;
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
        sprout.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            culture = Instantiate(GardenBed.CulturePrefab, transform).GetComponent<CultureBase>();
            culture.gardenBedCell = this;
            culture.GrowingUp(OnCultureGrowed);
            GardenBed.AddPlantedCell();
            sprout.DOScale(Vector3.zero, culture.TargetTime).SetEase(Ease.Linear);
        });
    }   

    public void OnHarvest()
    {
        culture = null;
        plowTimes = plowTimesMax;
        groundMesh.material = darkGrassMaterial;
        foreach (var item in GardenBed.GardenBedCells)
        {
            if (item.culture) return;
            else item.currentState = GardenBed.State.Plowing;
        }
        
        GardenBed.TryToNextState(currentState);
    }

    public void OnCultureGrowed()
    {
        currentState = GardenBed.State.Harvesting;
        GardenBed.TryToNextState(currentState);
    }

    void SetPlowed()
    {
        dirtFx.Emit();
        groundMesh.material = groundMaterial;
        currentState = GardenBed.State.Seeding;
        GardenBed.TryToNextState(currentState);
    }
}
