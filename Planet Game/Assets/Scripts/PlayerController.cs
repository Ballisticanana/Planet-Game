using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Body
    //Test
    //Update test
    //one last time ok lets go
    private Rigidbody playerRb;

    //Player controlls
    public float horizontalInput, forwardInput;

    //Player Settings
    public float playerSpeed;

    //World info
    private GameObject gameCenter;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameCenter = GameObject.Find("Game Center");
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(gameCenter.transform.forward * forwardInput * playerSpeed * Time.deltaTime);
        if (!Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0)
        {
            playerRb.AddForce(gameCenter.transform.right * horizontalInput * playerSpeed * Time.deltaTime);
        }
    }
}
