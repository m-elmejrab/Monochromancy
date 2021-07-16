using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	// Use this for initialization
	void awake () {
		
	}

    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.transform.tag != "Player")
            Destroy(gameObject);
    }
	
	
}
