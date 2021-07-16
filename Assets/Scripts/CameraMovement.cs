using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform player;

    private Vector3 offset ;
    private bool cameraAvailable = true;
    private int currentCamera = 0;
    private PlayerMovement playerMovement;
    private Vector3 targetPosition = new Vector3(0, 0, 0);

    private Vector3[] cameras= { new Vector3(0, 30, -12), new Vector3(12, 30, 0) , new Vector3(0, 30, 12), new Vector3(-12, 30, 0) };

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        offset = cameras[currentCamera];
        playerMovement.ChangeMovementDirection(currentCamera);

    }

    // Update is called once per frame
    void Update () {
        if(player==null)
        {

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            currentCamera = 0;
            offset = cameras[currentCamera];
            playerMovement.ChangeMovementDirection(0);

        }

        targetPosition = player.position + offset;


        if (Input.GetKey("q") && cameraAvailable)
        {
            player.Rotate(Vector3.up, -90);
            cameraAvailable = false;
            transform.RotateAround(player.transform.position, Vector3.up, -90f);
            Invoke("ChangeCameraAvailable", 0.5f);
            if(currentCamera==3)
            {
                currentCamera = 0;
                offset = cameras[currentCamera];
                playerMovement.ChangeMovementDirection(currentCamera);

            }
            else
            {
                currentCamera++;
                offset = cameras[currentCamera];
                playerMovement.ChangeMovementDirection(currentCamera);

            }            
        }


        if (Input.GetKey("e") && cameraAvailable)
        {
            player.Rotate(Vector3.up, 90);
            cameraAvailable = false;
            transform.RotateAround(player.transform.position, Vector3.up, 90f);
            Invoke("ChangeCameraAvailable", 0.5f);
            if (currentCamera == 0)
            {
                currentCamera = 3;
                offset = cameras[currentCamera];

                playerMovement.ChangeMovementDirection(currentCamera);

            }
            else
            {
                currentCamera--;
                offset = cameras[currentCamera];

                playerMovement.ChangeMovementDirection(currentCamera);
            }          

        }        

    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        transform.LookAt(player);
    }

    

    void ChangeCameraAvailable()
    {
        cameraAvailable = true;
    }
}
