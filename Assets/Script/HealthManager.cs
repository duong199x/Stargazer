using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public enum BodyStatus { NORMAL, INFECTED_HIDDEN, INFECTED_SHOWN, DEATH}

    private BodyStatus bodyStatus = BodyStatus.NORMAL;
    [SerializeField] private float maxHP;
    private float currentHP;
    [SerializeField] private float showInfectedThreshold;
    [SerializeField] private float healthDecreaseRateOnInfected;


    public void Init(float maxHP, float showInfectedThreshold, float healthDecreaseRateOnInfected)
    {
        this.bodyStatus = BodyStatus.NORMAL;
        this.maxHP = maxHP;
        this.showInfectedThreshold = showInfectedThreshold;
        this.healthDecreaseRateOnInfected = healthDecreaseRateOnInfected;
        this.currentHP = this.maxHP;
    }

    public void Die()
    {
        bodyStatus = BodyStatus.DEATH;
    }

    public void Update()
    {
        if (bodyStatus == BodyStatus.DEATH) return;
        if (bodyStatus == BodyStatus.INFECTED_HIDDEN || bodyStatus == BodyStatus.INFECTED_SHOWN)
        {
            currentHP -= healthDecreaseRateOnInfected;
            if (currentHP <= 0)
            {
                Die();
            }else if (currentHP < showInfectedThreshold)
            {
                bodyStatus = BodyStatus.INFECTED_SHOWN;
            }
        }
    }

    public void SetInfected()
    {
        bodyStatus = BodyStatus.INFECTED_HIDDEN;
    }

    public void Cure()
    {
        bodyStatus = BodyStatus.NORMAL;
        currentHP = maxHP;
    }
}
