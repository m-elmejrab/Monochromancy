using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDetector : MonoBehaviour
{
    public GameObject respawnObject;
    private bool playerInRange;

    // Use this for initialization
    void Awake()
    {
        Invoke("RequestRespawn", 0.2f);

    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.transform.tag == "Player")
            playerInRange = true;
    }

    void RequestRespawn()
    {

        RespawnObject obj = respawnObject.GetComponent<RespawnObject>();
        obj.ActivateRespwan(playerInRange);
        Destroy(gameObject);


    }
}
