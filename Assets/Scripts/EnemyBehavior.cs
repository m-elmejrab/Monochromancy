using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private GameObject player;
    private PlayerMovement pMovement;

    private int enemyHealth = 6;
    

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        pMovement = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.transform.tag == "Player")
            pMovement.DamagePlayer(4);

        if (colInfo.transform.tag == "Bullet")
        {
            DamageEnemy(2);
        }
    }

    private void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);         

        }
    }
}
