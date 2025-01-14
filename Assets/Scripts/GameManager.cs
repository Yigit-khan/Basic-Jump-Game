using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BoosterplatformPrefab;
    public GameObject killerplatformPrefab;
    public GameObject platformPrefab;
    public GameObject jumpCooldownResetPrefab;
    
    public GameObject jumpForceMultiplierPrefab;
    public GameObject character;

    public float startDistanceOffset = 5f;
    public float platformCount = 300;
    public float minDistanceBetweenPlatforms = 1.5f;
    public float boosterProbability = 0.1f;
    public float killerProbability = 0.05f;
    public float jumpCooldownResetProbability = 0.1f; 
    public float jumpForceMultiplierProbability = 0.1f;

    private List<Vector3> platformPositions = new List<Vector3>();

    void Start()
    {
        float startDistance = character.transform.position.y + startDistanceOffset;
        Vector3 spawnPosition = new Vector3(0, startDistance, 0);
        for (int i = 0; i < platformCount; i++)
        {
            bool positionIsValid = false;
            while (!positionIsValid)
            {
                spawnPosition.y += Random.Range(.1f, 2f);
                spawnPosition.x = Random.Range(-10f, 10f);

                positionIsValid = true;
                foreach (Vector3 pos in platformPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minDistanceBetweenPlatforms)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            }

            platformPositions.Add(spawnPosition);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            if (Random.value < killerProbability)
            {
                Vector3 killerPlatformPosition = spawnPosition + new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0);
                platformPositions.Add(killerPlatformPosition);
                Instantiate(killerplatformPrefab, killerPlatformPosition, Quaternion.identity);
            }

            if (Random.value < boosterProbability)
            {
                Vector3 boosterPlatformPosition = spawnPosition + new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0);
                platformPositions.Add(boosterPlatformPosition);
                Instantiate(BoosterplatformPrefab, boosterPlatformPosition, Quaternion.identity);
            }

            if (Random.value < jumpCooldownResetProbability)
            {
                Vector3 powerUpPosition = spawnPosition + new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0);
                platformPositions.Add(powerUpPosition);
                Instantiate(jumpCooldownResetPrefab, powerUpPosition, Quaternion.identity);
            }

            if (Random.value < jumpForceMultiplierProbability)
            {
                Vector3 powerUpPosition = spawnPosition + new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 3f), 0);
                platformPositions.Add(powerUpPosition);
                Instantiate(jumpForceMultiplierPrefab, powerUpPosition, Quaternion.identity);
            }
        }
    }
}
