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

    //Player controlls
    private float horizontalInput, forwardInput;
    private bool canJump = true;

    //Player Setting Variables
    public float playerSpeed;
    public float jumpStrength;
    public float jumpCooldownLength;

    //World info
    private GameObject gameCenter;
    public ParticleSystem planetGlow;
    //ParticleSystem.ColorOverLifetimeModule colorModule;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameCenter = GameObject.Find("Game Center");
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
                playerSavedJumpDirection = new Vector3(horizontalInput, 0, forwardInput).normalized;
                playerRb.velocity = Vector3.zero;
                playerRb.velocity = playerRb.velocity + playerSavedJumpDirection * jumpStrength;
                StartCoroutine("JumpCooldown");
            }
        }
            
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
    IEnumerator JumpCooldown()
    {
        //planetGlow.startColor = new Color(255, 255, 255, 255);
        canJump = false;
        yield return new WaitForSeconds(jumpCooldownLength);
        canJump = true;
    }
}
