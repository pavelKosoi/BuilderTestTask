using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GardenBedBase : MonoBehaviour
{
    public enum State
    {
        Plowing,
        Seeding,
        Growing,
        Harvesting
    }

    [SerializeField] Vector2Int gridSize;
    [SerializeField] float cellSize;
    [SerializeField] GameObject cellPrefab;
    List<GardenBedCell> gardenBedCells = new List<GardenBedCell>();
    [SerializeField] BoxCollider trigger;
    State currentState;

    Vector3 offset => new Vector3(gridSize.x * cellSize / 2.0f, 0, gridSize.y * cellSize / 2.0f);
    Vector3 startPos => transform.position - offset;


    private void OnValidate()
    {
        if (trigger) trigger.size = new Vector3(gridSize.x * cellSize, trigger.size.y, gridSize.y * cellSize);      
    }
    private void Start()
    {
       FillUpCells();
    }

    void FillUpCells()
    {
        foreach (var item in GetCellCenters())
        {
            GardenBedCell newCell = Instantiate(cellPrefab, item, Quaternion.identity, transform).GetComponent<GardenBedCell>();
            gardenBedCells.Add(newCell);
        }
    }

    public Vector3[] GetCellCenters()
    {
        Vector3[] cellCenters = new Vector3[gridSize.x * gridSize.y];
        int index = 0;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 cellCenter = startPos + new Vector3((x + 0.5f) * cellSize, 0, (y + 0.5f) * cellSize);
                cellCenters[index] = cellCenter;
                index++;
            }
        }

        return cellCenters;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            PlayerFarmingController playerFarming = other.gameObject.GetComponent<PlayerFarmingController>();
            if (playerFarming) playerFarming.StartDoingByState(currentState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            PlayerFarmingController playerFarming = other.gameObject.GetComponent<PlayerFarmingController>();
            if (playerFarming) playerFarming.BackToDefault();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var item in GetCellCenters())
        {
            Gizmos.DrawCube(item, new Vector3(cellSize, 0.1f, cellSize));
        }
        Gizmos.color = Color.blue;
        foreach (var item in GetCellCenters())
        {
            Gizmos.DrawWireCube(item, new Vector3(cellSize, 0.1f, cellSize));
        }
    }
}
