using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyScripts : MonoBehaviour
{
    //another test
    private Rigidbody enemyRb;
    public Vector3 enemyLocal;

    private GameObject player;
    private Rigidbody playerRb;
    public Vector3 playerLocal;
    private Vector3 awayFromPlayer;

    //Enemy 0 name: Basic Ball
    public float enemy0Speed = 1.0f;
    public float enemyV;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
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
            float enemyV = enemyRb.velocity.magnitude;
            Debug.Log(enemyV);

            if (enemyV > 3)
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
        Debug.Log("test");
        enemyRb.velocity = Vector3.zero;
        playerRb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        Debug.Log("riz");
        enemyRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
