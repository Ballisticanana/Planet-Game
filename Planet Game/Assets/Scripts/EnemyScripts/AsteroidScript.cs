using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;

    public float speed;

    public List<GameObject> asteroidShapePool;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        foreach (GameObject var in asteroidShapePool)
        {
            var.SetActive(false);
        }
        asteroidShapePool[Random.Range(0, 4)].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.AddForce((playerRb.position - enemyRb.position).normalized * speed * Time.deltaTime);
    }
}
