using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private int savedOtherPriorityInt;
    private int savedOtherEnemyLevel;

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
        transform.localScale = Vector3.one * ((enemyLevel / 3) + 1);
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

        if (other.CompareTag("Asteroid"))
        {
            savedOtherPriorityInt = other.GetComponent<AsteroidScript>().priorityInt;
            savedOtherEnemyLevel = other.GetComponent<AsteroidScript>().enemyLevel;
            if (savedOtherEnemyLevel > enemyLevel)
            {
                Destroy(gameObject);
            }
            else if (savedOtherEnemyLevel == enemyLevel && savedOtherPriorityInt > priorityInt)
            {
                Destroy(gameObject);
            }
            else if (savedOtherPriorityInt < priorityInt)
            {
                enemyLevel = enemyLevel + savedOtherEnemyLevel;
                transform.localScale = Vector3.one * (1+enemyLevel/2);
            }
        }
    }
}