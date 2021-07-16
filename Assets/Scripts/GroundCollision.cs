using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public int blockType;
    public int switchingIndex = 0;

    private bool fragileActivated = false;
    private float fragileDuration = 0f;
    private float fragileThreshold = 2f;

    // Use this for initialization
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fragileActivated == true)
        {
            fragileDuration += Time.deltaTime;
            CheckIfCollapsed();
        }
    }


    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.transform.tag == "Player")
        {
            if (gameObject.tag == "Fragile")
            {
                if (fragileActivated == false)
                    fragileActivated = true;
            }
        }

        if(colInfo.transform.tag=="FallPlane")
        {
            RespawnObject respawnScript = GetComponent<RespawnObject>();
            respawnScript.InitiateRespawn();
        }
    }
    void OnCollisionStay(Collision colInfo)
    {
        if (colInfo.transform.tag == "Player")
        {
            if (!playerMovement.isGrounded)
            {
                playerMovement.SetGrounded(true);
            }
        }

    }

    void OnCollisionExit(Collision colInfo)
    {
        if (colInfo.transform.tag == "Player")
        {
            playerMovement.SetGrounded(false);
        }
    }

    void CheckIfCollapsed()
    {
        if (fragileDuration >= fragileThreshold)
            gameObject.SetActive(false);
    }

}
