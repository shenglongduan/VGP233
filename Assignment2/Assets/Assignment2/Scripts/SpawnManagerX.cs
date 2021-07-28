using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;
    private float spawnInterval = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomInvoke());
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    //spawn interval a random value between 3 seconds and 5 seconds.
    IEnumerator RandomInvoke()
    {
        float interval = 1;
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnRandomBall();
            //get a random interval time between 3 seconds and 5 seconds.
            interval = Random.Range(3, 5);
        }
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        //get the random ball
        var ballPrefab = ballPrefabs[Random.Range(0, ballPrefabs.Length)];

        // instantiate ball at random spawn location
        Instantiate(ballPrefab, spawnPos, ballPrefab.transform.rotation);
    }
}