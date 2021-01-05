using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager = null;
    private float infectedValue = 0f;
    [SerializeField] private float infectedThreshold;
    [SerializeField] private float infectingRate;
    [SerializeField] private float infectingRadius;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    public void InfectedByOther(float value)
    {
        infectedValue += value;
        if (infectedValue > infectedThreshold)
        {
            healthManager.SetInfected();
        }
    }

    public void InfectingAround()
    {

    }

    public void Cure()
    {
        healthManager.Cure();
        infectedValue = 0f;
    }
}
