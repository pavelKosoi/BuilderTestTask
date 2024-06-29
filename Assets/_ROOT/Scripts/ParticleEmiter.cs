using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmiter : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void Emit()
    {
        if (particleSystems!=null)
        {
            foreach (var item in particleSystems)
            {
                ParticleSystem.EmissionModule emission = item.emission;
                item.Emit(Random.Range(emission.GetBurst(0).minCount, emission.GetBurst(0).maxCount));
            }
        }        
    }
}
