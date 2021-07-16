using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingTile : MonoBehaviour
{

    public GameObject originalTile;
    public GameObject newTile;

    private bool switchActivated = true;
    private float switchDuration = 0f;
    private float switchThreshold = 5f;
    private bool originalTileIsCurrent = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    

    void FixedUpdate()
    {
        if (switchActivated == true)
        {
            switchDuration += Time.deltaTime;
            if (switchDuration >= switchThreshold)
            {
                switchDuration = 0f;
                if (originalTileIsCurrent)
                {
                    originalTileIsCurrent = false;
                    Destroy(originalTile);
                    Instantiate(newTile, originalTile.transform.position, originalTile.transform.rotation);
                }
                else
                {
                    originalTileIsCurrent = true;
                    Destroy(newTile);
                    Instantiate(originalTile, originalTile.transform.position, originalTile.transform.rotation);
                }
            }
        }


    }

    public void StopSwitching()
    {
        switchActivated = false;
        Destroy(originalTile);
        Instantiate(newTile, originalTile.transform.position, originalTile.transform.rotation);
    }

}
