using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 10; // Base spawn rate (seconds)
    private float timer = 0;
    public float heightOffset = 10;
    public bird_script bird;

    private bool finalPipeSpawned = false;

    void Start()
    {
        // Auto-assign bird if not set in Inspector
        if (bird == null)
            bird = FindObjectOfType<bird_script>();

        spawnPipe();
    }

    void Update()
    {
        // Don't spawn pipes before the game starts
        if (!bird_script.gameStarted)
            return;

        // If bird is dead, spawn one more pipe if not already done, then stop
        if (bird == null || !bird.birdIsAlive)
        {
            if (!finalPipeSpawned)
            {
                spawnPipe();
                finalPipeSpawned = true;
            }
            return;
        }

        // Calculate spawn rate multiplier based on score thresholds: 10, 20, 40, 80, ...
        float spawnRateMultiplier = 1f;
        if (bird != null && bird.logic != null)
        {
            int threshold = 10;
            int multiplier = 0;
            int score = bird.logic.playerScore;
            while (score >= threshold)
            {
                multiplier++;
                threshold *= 2;
            }
            spawnRateMultiplier = Mathf.Pow(1f / 1.2f, multiplier); // Spawns are 20% faster at each threshold
        }

        float adjustedSpawnRate = spawnRate * spawnRateMultiplier;

        if (timer < adjustedSpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}