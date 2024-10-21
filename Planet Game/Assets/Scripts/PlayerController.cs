using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Player Body
    private Rigidbody playerRb;

    //Player controlls
    private float horizontalInput, forwardInput;

    //Player Setting Variables
    public float playerSpeed;

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
}
