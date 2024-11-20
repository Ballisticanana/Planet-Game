using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;

    private Renderer m_Renderer;
    public List<Texture> texturePool;

    public int stage;
    public bool readyForNewPosition;
    public Vector3 attackPosition;
    public bool moveToNewPosition;
    public float moveForce;
    public float slowRait;
    public bool atAttackPosition;
    public bool inAttackCharge;
    public Vector3 enemySwapVectorDirection;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.SetTexture("_BaseMap", texturePool[Random.Range(0, 3)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (stage == 1)
        {
            enemySwapVectorDirection = new Vector3(-playerRb.position.x / Mathf.Abs(playerRb.position.x), playerRb.position.y, -playerRb.position.z / Mathf.Abs(playerRb.position.z));
            attackPosition = new Vector3(Random.Range(5, 13) * enemySwapVectorDirection.x, playerRb.position.y, Random.Range(5, 13) * enemySwapVectorDirection.z);
            stage = 2;
        }
        if (stage == 2)
        {
            enemyRb.AddForce((attackPosition - enemyRb.position).normalized * moveForce * Time.deltaTime);
        }
        if (stage == 2 && Vector3.Distance(enemyRb.position, attackPosition) < 1)
        {
            // fix slow rait
            enemyRb.velocity = enemyRb.velocity / 1.02f;
            if (enemyRb.velocity.magnitude < 0.1f)
            {
                enemyRb.velocity = Vector3.zero;
                stage = 3;
            }
        }
        if (stage == 3)
        {

            var tempV = Quaternion.LookRotation(playerRb.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, tempV, 40000000 * Time.deltaTime);
            //transform.rotation.x, tempV.y, transform.rotation.z
            Debug.DrawLine(enemyRb.position, playerRb.position, Color.cyan);
            //enemyRb.transform.LookAt(playerRb.position);

            //  enemyRb.transform.LookAt(playerRb.position);
        }

    }
}
