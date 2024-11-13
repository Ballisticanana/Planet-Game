using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Player Body
    private Rigidbody playerRb;
    private Vector3 playerSavedJumpDirection;
    private Vector3 directionSwitch;
    private float playerSavedJumpMagnitude;

    //Player controlls
    private float horizontalInput, forwardInput;
    private bool canJump = true;

    //Player Setting Variables
    public float playerSpeed;
    public float jumpStrength;
    public float jumpCooldownLength;

    //World info
    private GameObject gameCenter;
    private SpawnManager spawnManager;
    public ParticleSystem planetGlow;
    //ParticleSystem.ColorOverLifetimeModule colorModule;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameCenter = GameObject.Find("Game Center");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //planetGlow
    }

    void Update()
    {
        if (canJump == true)
        {
            //planetGlow.MainModule.startColor = new Color(95, 255, 0 ,255);
            //planetGlow.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                directionSwitch = new Vector3(horizontalInput, 1, forwardInput);
                playerSavedJumpDirection = playerRb.velocity.normalized;
                playerSavedJumpMagnitude = playerRb.velocity.magnitude;
                playerRb.velocity = Vector3.zero;
                playerRb.velocity = playerSavedJumpMagnitude * playerSavedJumpDirection * jumpStrength;  
                StartCoroutine("JumpCooldown");
            }
        }
            
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().EnemyMoonGameObjectRetrieve(new Vector3(Random.Range(-10,11),0.75f, Random.Range(-10, 11)));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().EnemyAsteroidGameObjectRetrieve(new Vector3(Random.Range(-10, 11), 6, Random.Range(-10, 11)), Random.Range(1,5));
        }

        if (Input.GetKeyDown(KeyCode.R) || Vector3.Distance(new Vector3(0,0,0), playerRb.transform.position) > 30)
        {
            StartCoroutine("Reset");
        }
        #region Player W,A,S,D
        // Assign values to each float
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        // Moves player forward and backwards relitive to the game centers faceing direction
        playerRb.AddForce(gameCenter.transform.forward * forwardInput * playerSpeed * Time.deltaTime);
        // Moves player left and right relitive to the game centers faceing direction & if shift is not pressed
        if (!Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0)
        {
            playerRb.AddForce(gameCenter.transform.right * horizontalInput * playerSpeed * Time.deltaTime);
        }
    #endregion
    }
    public void Reset()
    {
        //Debug.Log("Reseting");
        foreach (GameObject impactParticleNumber in spawnManager.impactParticlePool)
        {
            //ImpactParticleReturn(0, impactParticle);
            StartCoroutine(spawnManager.ImpactParticleReturnInstant(impactParticleNumber));
        }
        foreach (GameObject enemyOnEnemyParticleNumber in spawnManager.enemyOnEnemyParticlePool)
        {
            StartCoroutine(spawnManager.EnemyOnEnemyParticleReturnInstant(enemyOnEnemyParticleNumber));
        }
        foreach (GameObject asteroidExplosionParticleNumber in spawnManager.asteroidExplosionParticlePool)
        {
            StartCoroutine(spawnManager.AsteroidExplosionParticleReturnInstant(asteroidExplosionParticleNumber));
        }
        foreach (GameObject enemyMoonGameObject in spawnManager.enemyMoonGameObjectPool)
        {
            enemyMoonGameObject.GetComponent<MoonScript>().ResetToPool();
        }
        foreach (GameObject enemyAsteroidGameObject in spawnManager.enemyAsteroidGameObjectPool)
        {
            enemyAsteroidGameObject.GetComponent<AsteroidScript>().ResetToPool();
        }
        playerRb.velocity = Vector3.zero;
        gameCenter.GetComponent<GameCenter>().waveNumber = 0;
        StartCoroutine(gameCenter.GetComponent<GameCenter>().SpawnWave());
    }
    IEnumerator JumpCooldown()
    {
        //planetGlow.startColor = new Color(255, 255, 255, 255);
        canJump = false;
        yield return new WaitForSeconds(jumpCooldownLength);
        canJump = true;
    }
}
