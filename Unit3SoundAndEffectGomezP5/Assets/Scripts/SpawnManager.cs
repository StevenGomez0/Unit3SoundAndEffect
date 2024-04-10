using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float startDelay = 2;
    private float repeatRate = 2;
    private Vector3 spawnpos = new Vector3(25, 0, 0);
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerController.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnpos, obstaclePrefab.transform.rotation);
        }
    }
}