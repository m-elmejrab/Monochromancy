using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{


    private GameObject[] originalTiles;
    public int switchingIndex;
    public GameObject newTile;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

    }

    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.transform.tag == "Player")
        {
            originalTiles = GameObject.FindGameObjectsWithTag("SwitchingTile");
            foreach(GameObject orTile in originalTiles)
            {
                GroundCollision gCol = orTile.gameObject.GetComponent<GroundCollision>();
                if(gCol.switchingIndex==this.switchingIndex)
                {
                    Destroy(orTile);
                    Instantiate(newTile, orTile.transform.position, orTile.transform.rotation);
                }
                
            }
            
            
        }
        
    }

}
