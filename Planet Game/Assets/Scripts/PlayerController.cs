using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Player Body
    private Rigidbody playerRb;

    //Player controlls
    private float horizontalInput, forwardInput;

    //Player Setting Variables
    public float playerSpeed;
    public float jumpStrength;

    //World info
    private GameObject gameCenter;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameCenter = GameObject.Find("Game Center");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().EnemyMoonGameObjectRetrieve(new Vector3(Random.Range(-10,11),0.75f, Random.Range(-10, 11)));
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
        Debug.Log("Reseting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
