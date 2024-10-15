using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyScripts : MonoBehaviour
{
    //another test
    private Rigidbody enemyRb;
    public Vector3 enemyLocal;

    private GameObject player;
    public Vector3 playerLocal;

    //Enemy 0 name: Basic Ball
    public float enemy0Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerLocal = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        enemyLocal = new Vector3(transform.position.x, 0, transform.position.z);
        enemyRb.AddForce((playerLocal - enemyLocal).normalized * enemy0Speed * Time.deltaTime);
    }
}
