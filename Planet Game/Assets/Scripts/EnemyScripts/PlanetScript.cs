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
        
    }
}
