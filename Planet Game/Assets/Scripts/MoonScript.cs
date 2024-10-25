using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    #region Variables
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
    private bool disableEnemyMovement = false;

    public float platformPullbackRadius;
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
    void Start()
    {
        disableEnemyMovement = false;
        enemyRb = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        otherEnemyRb = GameObject.Find("Moon").GetComponent<Rigidbody>();


        //fix renderer
        m_Renderer.material.SetTexture("_MainTex", texturePool[Random.Range(0, 3)]);


        gameCenter = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        #region Enemy Movement
        if (disableEnemyMovement == false)
        {
            playerLocal = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            enemyLocal = new Vector3(transform.position.x, 0, transform.position.z);
            enemyRb.AddForce((playerLocal - enemyLocal).normalized * enemy0Speed * Time.deltaTime);
            if (Vector3.Distance(gameCenter, enemyRb.transform.position) > platformPullbackRadius)
            {
                enemyRb.AddForce((gameCenter - enemyRb.transform.position).normalized * platformPullbackStrength * Time.deltaTime);
            }
        }
        #endregion
        if (enemyRb.transform.position.y < -75)
        {
            enemyRb.velocity = Vector3.zero;
            m_Renderer.material.SetTexture("_MainTex", texturePool[Random.Range(0, 3)]);
            gameObject.SetActive(false);
            enemyRb.transform.position = new Vector3();
            disableEnemyMovement = false;

        }
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
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;
            float impactForce = enemyRb.velocity.magnitude;
            if (Mathf.Abs((playerRb.velocity - enemyRb.velocity).magnitude) > neededImpactForFreeze && enemyCanBeHit == true)
            {
                Debug.Log("Needed impact for a freeze " + neededImpactForFreeze + " m/s          Impact force " + ((Mathf.Round(enemyRb.velocity.magnitude * 10)) / 10) + " m/s");
                StartCoroutine(ImpactFreeze());
            }
            else
            {
                enemyRb.AddForce(awayFromPlayer * normalImpactForce, ForceMode.Impulse);
                playerRb.AddForce(Vector3.down * 1000);
                playerRb.AddForce(((playerRb.velocity - enemyRb.velocity).magnitude) * -awayFromPlayer.normalized * playerGivenKnockback, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Enemy 0"))
        {
            if (Mathf.Abs((otherEnemyRb.velocity - enemyRb.velocity).magnitude) > 25)
            {
                Debug.Log("enemys hit");
                GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().EnemyOnEnemyParticleRetrieve((otherEnemyRb.transform.position + enemyRb.transform.position) / 2);
                enemyRb.velocity = Vector3.zero;
                m_Renderer.material.SetTexture("_MainTex", texturePool[Random.Range(0, 3)]);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ImpactFreeze()
    {
        enemyCanBeHit = false;
        Vector3 saveAwayFromPlayer = transform.position - playerRb.gameObject.transform.position;
        float savedEnemyVelocity = enemyRb.velocity.magnitude;
        enemyRb.velocity = Vector3.zero;
        playerRb.AddForce(Vector3.down * 1500);
        GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().ImpactParticleRetrieve((playerRb.transform.position + enemyRb.transform.position) / 2);
        enemyRb.isKinematic = true;
        Debug.Log("waitForSeconds * savedEnemyVelocity / neededImpactForFreeze = " + waitForSeconds + " * " + savedEnemyVelocity + " / " + neededImpactForFreeze + " = " + waitForSeconds * savedEnemyVelocity / neededImpactForFreeze + " = Freeze time");
        yield return new WaitForSeconds(waitForSeconds * savedEnemyVelocity / neededImpactForFreeze);
        enemyRb.isKinematic = false;
        enemyRb.AddForce(saveAwayFromPlayer * savedEnemyVelocity * ImpactFreezeForce, ForceMode.Impulse);
        enemyCanBeHit = true;
    }
}