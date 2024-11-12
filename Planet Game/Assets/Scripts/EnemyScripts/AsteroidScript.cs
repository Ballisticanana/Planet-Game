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

    public float speed;
    public int enemyLevel;
    public bool disableEnemyMovement = false;
    public int priorityInt;
    public bool canFire = true;

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
        transform.localScale = Vector3.one * (0.5f + 0.5f * enemyLevel);
    }

    void Update()
    {
        if(disableEnemyMovement == false)
        {            
            if (Vector3.Distance(playerRb.position, enemyRb.position) > 6)
            {
                enemyRb.AddForce((playerRb.position - enemyRb.position).normalized * speed * Time.deltaTime * enemyLevel);
                if (enemyRb.transform.position.y < 6)
                {
                    enemyRb.AddForce(Vector3.up * 5);
                }
                else if (enemyRb.transform.position.y > 7)
                {
                    enemyRb.AddForce(Vector3.down * 5);
                }
            }
            else
            {
                if (canFire == true)
                StartCoroutine(FireAtPlayer());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnManager.AsteroidExplosionParticleRetrieve((playerRb.transform.position + enemyRb.transform.position)/2);
            ResetToPool();
            playerRb.AddForce(new Vector3(Random.Range(-1, 1) + 0.5f, 0, Random.Range(-1, 1) + 0.5f).normalized * enemyLevel * 20, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerRb.AddExplosionForce(500 * enemyLevel, transform.position, 10);
            spawnManager.AsteroidExplosionParticleRetrieve(enemyRb.transform.position);
            ResetToPool();
        }
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            savedOtherPriorityInt = collision.gameObject.GetComponent<AsteroidScript>().priorityInt;
            savedOtherEnemyLevel = collision.gameObject.GetComponent<AsteroidScript>().enemyLevel;
            if (savedOtherEnemyLevel > enemyLevel)
            {
                //do nothing
            }
            else if (savedOtherEnemyLevel == enemyLevel && savedOtherPriorityInt > priorityInt)
            {
                //do nothing
            }
            else if (savedOtherPriorityInt < priorityInt && enemyLevel + savedOtherEnemyLevel < 5)
            {
                enemyLevel = enemyLevel + savedOtherEnemyLevel;
                transform.localScale = Vector3.one * (0.5f + 0.5f * enemyLevel);
                collision.gameObject.SetActive(false);
                spawnManager.EnemyOnEnemyParticleRetrieve(transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Under Platform"))
        {
            disableEnemyMovement = true;
        }
    }
    public IEnumerator FireAtPlayer()
    {
        canFire = false;
        enemyRb.AddForce((playerRb.position - enemyRb.position).normalized * 20, ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        canFire = true;
    }
    public void ResetToPool()
    {
        priorityInt = Random.Range(0, 1000000000);
        enemyRb.velocity = Vector3.zero;
        foreach (GameObject var in asteroidShapePool)
        {
            var.SetActive(false);
        }
        asteroidShapePool[Random.Range(0, 4)].SetActive(true);
        gameObject.SetActive(false);
        canFire = true;
        disableEnemyMovement = false;
    }
}