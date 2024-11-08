using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;

    public int asteroidClumpSize;
    public float speed;
    public int enemyLevel;
    private bool disableEnemyMovement = false;
    private int priorityInt;

    public List<GameObject> asteroidShapePool;

    void Start()
    {
        priorityInt = Random.Range(0, 1000000000);
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

    void Update()
    {
        enemyRb.AddForce((playerRb.position - enemyRb.position).normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I Should Blow up now");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Under Platform"))
        {
            disableEnemyMovement = true;
        }
        if (other.CompareTag("Asteroid") && other.GetComponent<AsteroidScript>().priorityInt > priorityInt)
        {
            //die function
        }  
        else
        {
            enemyLevel++;
            //scale script
        }
    }
}
