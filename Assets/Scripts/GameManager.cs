using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject character;
    
    public float startDistanceOffset = 5f;
    public float platformCount = 300;
    void Start()
    {
        float startDistance = character.transform.position.y + startDistanceOffset;
        Vector3 spawnPosition = new Vector3(0, startDistance, 0);
        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(.1f, 2f);
            spawnPosition.x = Random.Range(-10f, 10f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
