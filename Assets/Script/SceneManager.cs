using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Vector2 randomPosCenter = new Vector2();
    [SerializeField] private Vector2 randomSize = new Vector2();

    [SerializeField] private Sprite[] listPeopleSprite;
    [SerializeField] private GameObject personPrefab;

    [SerializeField] private int numOfPeople;
    [SerializeField] private float spawnPosZ;
    [SerializeField] private float initPersonScale;

    private void Awake()
    {
        Vector3 spawnPos = new Vector3();
        spawnPos.z = spawnPosZ;
        GameObject person = null;
        SpriteRenderer personSpriteRenderer = null;
        int randomSpriteIndex;
        for (int i = 0; i < numOfPeople; i++)
        {
            spawnPos.x = randomPosCenter.x + Random.Range(-1.0f, 1.0f) * randomSize.x / 2.0f;
            spawnPos.y = randomPosCenter.y + Random.Range(-1.0f, 1.0f) * randomSize.y / 2.0f;
            person = Instantiate(personPrefab, spawnPos, Quaternion.identity);
            person.transform.localScale = Vector3.one * initPersonScale;
            personSpriteRenderer = person.GetComponentInChildren<SpriteRenderer>();
            randomSpriteIndex = (int)(Random.value * listPeopleSprite.Length);
            if (personSpriteRenderer) personSpriteRenderer.sprite = listPeopleSprite[randomSpriteIndex];
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color drawColor = Color.white;
        drawColor.a = 1f;
        Gizmos.color = drawColor;
        Gizmos.DrawWireCube((Vector3)randomPosCenter, (Vector3)randomSize);
    }
}
