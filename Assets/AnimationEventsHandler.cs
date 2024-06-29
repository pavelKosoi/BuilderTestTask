using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    PlayerFarmingController farmingController;

    private void Awake()
    {
        farmingController = GetComponentInParent<PlayerFarmingController>();
    }

    public void WorkingEvent()
    {
        farmingController.Work();
    }
}
