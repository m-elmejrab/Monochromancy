using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour {

    public Transform objectRespawnPostion;
    public Vector3 playerRespawnOffset;
    public GameObject respawnDetector;
    private GameObject player;
    private Vector3 savedRespawnPostion;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        savedRespawnPostion = objectRespawnPostion.position;


    }

	public void ActivateRespwan(bool playerInRange)
    {
        if(playerInRange)
        {
            player.transform.position = savedRespawnPostion + playerRespawnOffset;
            gameObject.transform.position = savedRespawnPostion;
        }
        else
        {
            gameObject.transform.position = savedRespawnPostion;
        }

    }

    public void InitiateRespawn()
    {
        GameObject detector = Instantiate(respawnDetector,savedRespawnPostion + new Vector3(0,0.5f,0),objectRespawnPostion.rotation) as GameObject;
        RespawnDetector detectorScript = detector.GetComponent<RespawnDetector>();
        detectorScript.respawnObject = gameObject;
        
    }
}
