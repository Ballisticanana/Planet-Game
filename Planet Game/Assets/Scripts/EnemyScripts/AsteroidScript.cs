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
    public float canFireTimeFix = 0;

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
        if (disableEnemyMovement == false)
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
        #region Can Fire Fix
        if (canFire == false)
        {
            canFireTimeFix -= Time.deltaTime;
        }
        else if(canFire == true && canFireTimeFix != 2)
        {
            canFireTimeFix = 2;
        }
        if(canFireTimeFix < 0)
        { 
            canFire = true;
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnManager.AsteroidExplosionParticleRetrieve(((playerRb.transform.position + enemyRb.transform.position)/2), enemyLevel);
            ResetToPool();
            playerRb.AddForce(new Vector3(Random.Range(-1, 1) + 0.5f, 0, Random.Range(-1, 1) + 0.5f).normalized * enemyLevel * 20, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerRb.AddExplosionForce(500 * enemyLevel, transform.position, 10);
            Rigidbody[] everythingWithRb;
            everythingWithRb = FindObjectsOfType<Rigidbody>();
            foreach(Rigidbody var in everythingWithRb)
            {
                var.AddExplosionForce(500 * enemyLevel, transform.position, 8 + 2*enemyLevel);
            }
            spawnManager.AsteroidExplosionParticleRetrieve(enemyRb.transform.position, enemyLevel);
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
            StartCoroutine(WaitTwoSeconds());
        }
    }
    public IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2);
        spawnManager.AsteroidExplosionParticleRetrieve(enemyRb.transform.position, enemyLevel);
        ResetToPool();
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