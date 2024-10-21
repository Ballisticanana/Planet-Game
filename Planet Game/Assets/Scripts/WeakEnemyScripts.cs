using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyScripts : MonoBehaviour
{
    private Rigidbody enemyRb;
    public Vector3 enemyLocal;
    private GameObject player;
    private Rigidbody playerRb;
    public Vector3 playerLocal;
    private Vector3 awayFromPlayer;
    public float enemy0Speed = 1.0f;
    public float ImpactForce;
    public ParticleSystem clashImpact;
    private SpawnManager spawnManager;
    private Vector3 impactPoint;
    public bool enemyCanBeHit = true;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        //spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().ClashImpact();
    }

    // Update is called once per frame
    void Update()
    {
        playerLocal = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        enemyLocal = new Vector3(transform.position.x, 0, transform.position.z);
        enemyRb.AddForce((playerLocal - enemyLocal).normalized * enemy0Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;
            float impactForce = enemyRb.velocity.magnitude;
            Debug.Log(ImpactForce);

            if (impactForce > 25f && enemyCanBeHit == true)
            {
                StartCoroutine(ImpactFreeze());
            }else
            {
                enemyRb.AddForce(awayFromPlayer * 1.5f, ForceMode.Impulse);
            }
        }
    }

    IEnumerator ImpactFreeze()
    {
        enemyCanBeHit = false;
        Vector3 saveAwayFromPlayer = transform.position - playerRb.gameObject.transform.position;
        float saveEnemyV = enemyRb.velocity.magnitude;
        enemyRb.velocity = Vector3.zero;
        impactPoint = (playerRb.transform.position + enemyRb.transform.position)/ 2;
        GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().ImpactParticleRetrieve((playerRb.transform.position + enemyRb.transform.position) / 2);
        enemyRb.isKinematic = true;
        yield return new WaitForSeconds(0.3f);
        enemyRb.isKinematic = false;
        enemyRb.AddForce(saveAwayFromPlayer * saveEnemyV * 0.3f, ForceMode.Impulse);
        enemyCanBeHit = true;
    }
}
