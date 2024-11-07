using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    #region Variables
    //private variables
    private Rigidbody enemyRb;
    private Vector3 enemyLocal;
    private GameObject player;
    private Rigidbody playerRb;
    private Rigidbody otherEnemyRb;
    private Vector3 otherEnemyPos;
    private Vector3 playerLocal;
    private Vector3 awayFromPlayer;
    private SpawnManager spawnManager;
    private Vector3 impactPoint;
    private bool enemyCanBeHit = true;
    private Vector3 gameCenter;
    public bool disableEnemyMovement = true;
    private int swarmBonus;
    private float pullBackTimer;
    private bool canPullBack = true;

    //public bools
    public float swarmBonusRange;
    public float platformPullbackRadiusMax;
    public float platformPullbackRadiusMin;
    public float platformPullbackStrength;
    public float enemy0Speed = 1.0f;
    public float normalImpactForce;
    public float playerGivenKnockback;
    public float ImpactFreezeForce;
    public float neededImpactForFreeze = 25f;
    public float waitForSeconds = 0.3f;

    //Texture stuff
    private Renderer m_Renderer;
    public List<Texture> texturePool;
    #endregion
    #region Start
    void Start()
    {
        //pretty sure this is redundant
        //disableEnemyMovement = false;
        //access self components
        enemyRb = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
        //access GameObject components
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //assign random texture when instantiated 
        m_Renderer.material.SetTexture("_BaseMap", texturePool[Random.Range(0, 3)]);
        //Set game center
        //NOTE change all 0,0,0 too this
        gameCenter = new Vector3(0, 0, 0);
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        #region Enemy Movement
        //if enemy can move
        if (disableEnemyMovement == false)
        {
            //find player and enemy location
            //NOTE use these variables instead of the whole line each time
            playerLocal = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            enemyLocal = new Vector3(transform.position.x, 0, transform.position.z);
            //moves enemy at player 
            enemyRb.AddForce((playerLocal - enemyLocal).normalized * enemy0Speed * Time.deltaTime);
            //if the enemy get too close to the edge of the board it forces the enemy back
            //NOTE rework so theres a smoother transition to the end of the board and not a abrupt stop
            if (Vector3.Distance(gameCenter, enemyRb.transform.position) > platformPullbackRadiusMax && pullBackTimer <= 0 && canPullBack == true)
            {
                //Debug.Log(Vector3.Distance(gameCenter, enemyRb.transform.position) - platformPullbackRadius);
                //Debug.Log(platformPullbackRadius);
                //NOTE can this be written better?
                enemyRb.AddForce(((gameCenter - enemyRb.transform.position).normalized) * platformPullbackStrength * Time.deltaTime * -((Vector3.Distance(gameCenter, enemyRb.transform.position) - platformPullbackRadiusMin) / platformPullbackRadiusMax - platformPullbackRadiusMin));
            }
        }
        #endregion
        //if the enemy is below this point rest to pool
        if (enemyRb.transform.position.y < -55)
        {
            ResetToPool();
        }
        pullBackTimer = pullBackTimer - Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Under Platform"))
        {
            disableEnemyMovement = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherEnemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.CompareTag("Player"))
        {
            pullBackTimer = 1.5f;
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;
            float impactForce = enemyRb.velocity.magnitude;
            //NOTE should only impact particle if player is moving 2/3 as fast as rnemy or more
            if (Mathf.Abs((playerRb.velocity - enemyRb.velocity).magnitude) > neededImpactForFreeze && enemyCanBeHit == true)
            {
                //Debug.Log("Needed impact for a freeze " + neededImpactForFreeze + " m/s          Impact force " + ((Mathf.Round(enemyRb.velocity.magnitude * 10)) / 10) + " m/s");
                enemyCanBeHit = false;
                float savedEnemyVelocity = enemyRb.velocity.magnitude;
                enemyRb.velocity = Vector3.zero;
                spawnManager.ImpactParticleRetrieve((playerRb.transform.position + enemyRb.transform.position) / 2);
                //Debug.Log("waitForSeconds * savedEnemyVelocity / neededImpactForFreeze = " + waitForSeconds + " * " + savedEnemyVelocity + " / " + neededImpactForFreeze + " = " + waitForSeconds * savedEnemyVelocity / neededImpactForFreeze + " = Freeze time");
                enemyRb.AddForce((transform.position - playerRb.gameObject.transform.position) * savedEnemyVelocity * ImpactFreezeForce, ForceMode.Impulse);
                enemyCanBeHit = true;
            }
            else
            {
                enemyRb.AddForce(awayFromPlayer * normalImpactForce, ForceMode.Impulse);
                //NOTE can this be redon to disable y movement for the impact instance
                playerRb.AddForce(Vector3.down * playerGivenKnockback * swarmBonus, ForceMode.Impulse);
                swarmBonus = 0;
                foreach (GameObject enemy in spawnManager.enemyMoonGameObjectPool)
                {
                    if (enemy.activeInHierarchy && Vector3.Distance(enemy.transform.position, transform.position) < 5)
                    {
                        swarmBonus++;
                    }
                }
                playerRb.AddForce(((playerRb.velocity - enemyRb.velocity).magnitude) * -awayFromPlayer.normalized * playerGivenKnockback * swarmBonus, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Enemy 0"))
        {
            if (Mathf.Abs((otherEnemyRb.velocity - enemyRb.velocity).magnitude) > 40)
            {
                //Debug.Log("enemys hit");
                spawnManager.EnemyOnEnemyParticleRetrieve((otherEnemyRb.transform.position + enemyRb.transform.position) / 2);
                ResetToPool();
            }
        }
        if (collision.gameObject.CompareTag("Ground") && disableEnemyMovement == true && transform.position.y > 0)
        {
            disableEnemyMovement = false;
            spawnManager.ImpactParticleRetrieve(transform.position - new Vector3(0, 0.5f, 0));
        }
    }
    public void ResetToPool()
    {
        //Resets pullback timer to zero
        pullBackTimer = 0;
        //rest velocity
        enemyRb.velocity = Vector3.zero;
        //reset texture
        m_Renderer.material.SetTexture("_BaseMap", texturePool[Random.Range(0, 3)]);
        //turns movement back on for when its reactivated
        disableEnemyMovement = true;
        //NOTE make this go to spawn manager DONE
        enemyRb.transform.position = spawnManager.transform.position;
        //diaable game object 
        gameObject.SetActive(false);
    }
}
