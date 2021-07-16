using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDetector : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter(Collider obj)
    {
        if (obj.transform.tag == "Player")
            GameManager.instance.l0CheckpointPassed();
    }
}
