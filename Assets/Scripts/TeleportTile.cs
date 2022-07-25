using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour {

    GameObject player;
    public Transform targetPosition;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision colInfo)
    {
        if(colInfo.transform.tag == "Player" && targetPosition != null)
			player.transform.position = targetPosition.position+ new Vector3(0,1,0);
    }
}
