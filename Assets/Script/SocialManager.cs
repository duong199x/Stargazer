using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager : MonoBehaviour
{
    public static int NUM_OF_PROPERTIES = 4;
    [SerializeField] private float[] characteristicVector;
    [SerializeField] private float socialFactor = 1f;

    public float[] CharacteristicVector { get => characteristicVector;}
    public float SocialFactor { get => socialFactor;}

    private void Awake()
    {
        characteristicVector = new float[NUM_OF_PROPERTIES];
        for (int i=0;i<NUM_OF_PROPERTIES; i++)
        {
            characteristicVector[i] = Random.value;
        }
        socialFactor = (Random.value < 0.9f) ? 1.0f : -1.0f;
    }
}
