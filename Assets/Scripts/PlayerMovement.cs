using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool isGrounded = true;
    public Material[] colors;
    public Text healthText;
    public Text mainGameText;
    public GameObject levelImage;
    public int playerHealth = 10;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float speed = 8f;
    public float jumpSpeed = 6f;
    public float moveInJumpSpeed = 30f;
    public Image damageImage;
    public float jumpHeight = 1.4f;
    public float bulletSpeed = 2f;


    private MeshRenderer playerMesh;
    private int currentState;
    private bool jump = false;
    private bool jumpAvailable = true;
    private bool shoot = false;
    private bool shootAvailable = true;
    private float timeInMidair = 0f;
    private bool damaged = false;
    private Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    private float flashSpeed = 1f;
    private Vector3 movement;
    private Vector3 jumpMovement;
    private int direction;
    private AudioSource jumpSound;
    private AudioSource shootSound;
    private bool itemCollectAvailable = true;



    // Use this for initialization
    void Awake()
    {
        playerMesh = GetComponent<MeshRenderer>();
        currentState = 0; // Hero is black
        ChangeState();
        direction = 0;
        Invoke("EarlyMessage", 1f);
        List<AudioSource> sounds = GetComponents<AudioSource>().ToList<AudioSource>();
        jumpSound = sounds[0];
        shootSound = sounds[1];



    }

    void Start()
    {
        gameObject.transform.position = GameManager.instance.positionPlayer();

    }
    private void EarlyMessage()
    {
        GameManager.instance.DisplayLongInfoText(//"Welcome to the journey of miserable Libyans who just didn't give up!!" +
            "WASD: Move, Space: Jump, R: Shoot, Q & E: Rotate Camera");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("space") && isGrounded && jumpAvailable)
        {
            jump = true;
            jumpAvailable = false;
            jumpSound.Play();
        }

        if (Input.GetKey("r") && shootAvailable == true)
        {
            shoot = true;
            shootAvailable = false;
            shootSound.Play();
        }

        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (isGrounded)
            Move(h, v, direction);
        else
            MoveInJump(h, v, direction);

        if (jump == true)
        {
            //jump = false;
            Invoke("ReenableJump", 1f);
            //rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.Impulse);
            Jump(h, v, direction);
        }


        if (shoot == true)
        {
            shoot = false;
            Invoke("ReenableShoot", 0.2f);
            Shoot();
        }

        if (!isGrounded)
        {
            rb.AddForce(0, -40, 0, ForceMode.Acceleration);
            timeInMidair += Time.deltaTime;
        }

    }

    private void Move(float h, float v, int direction)
    {
        switch (direction)
        {
            case 0:
                movement.Set(h, 0, v);
                break;

            case 1:
                movement.Set(-v, 0, h);
                break;

            case 2:
                movement.Set(-h, 0, -v);
                break;

            case 3:
                movement.Set(v, 0, -h);
                break;

        }
        movement = movement * Time.deltaTime * speed;
        rb.MovePosition(transform.position + movement);
    }

    private void MoveInJump(float h, float v, int direction)
    {
        switch (direction)
        {
            case 0:
                //movement.Set(h, 0, v);
                jumpMovement.Set(h, 0, v);
                break;

            case 1:
                jumpMovement.Set(-v, 0, h);
                //movement.Set(-v, 0, h);
                break;

            case 2:
                jumpMovement.Set(-h, 0, -v);
                //movement.Set(-h, 0, -v);
                break;

            case 3:
                jumpMovement.Set(v, 0, -h);
                //movement.Set(v, 0, -h);
                break;
        }
        jumpMovement = (jumpMovement * moveInJumpSpeed * Time.deltaTime) - (jumpMovement * moveInJumpSpeed * Time.deltaTime * timeInMidair);
        //rb.MovePosition(transform.position + jumpMovement);
        rb.AddForce(jumpMovement, ForceMode.VelocityChange);
    }


    private void Jump(float h, float v, int direction)
    {
        jump = false;
        switch (direction)
        {
            case 0:
                //movement.Set(h, 0, v);
                jumpMovement.Set(h, jumpHeight, v);
                break;

            case 1:
                jumpMovement.Set(-v, jumpHeight, h);
                //movement.Set(-v, 0, h);
                break;

            case 2:
                jumpMovement.Set(-h, jumpHeight, -v);
                //movement.Set(-h, 0, -v);
                break;

            case 3:
                jumpMovement.Set(v, jumpHeight, -h);
                //movement.Set(v, 0, -h);
                break;
        }
        jumpMovement.Set(0, jumpHeight, 0);
        jumpMovement = jumpMovement * jumpSpeed;//* Time.deltaTime;
        //rb.MovePosition(transform.position + jumpMovement);
        rb.AddForce(jumpMovement, ForceMode.VelocityChange);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 1.5f);
    }

    public void ChangeMovementDirection(int direction)
    {
        this.direction = direction;

    }

    private void ReenableJump()
    {
        jumpAvailable = true;
    }

    private void ReenableShoot()
    {
        shootAvailable = true;
    }


    private void ChangeState()
    {
        if (colors.Length <= 0)
            return;
        else
        {
            playerMesh.material = colors[currentState];
        }
    }

    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.transform.tag == "Finish")
        {
            if (GameManager.instance.GetKeyCount() == 2)
            {
                mainGameText.text = "Unfortunately you won!!";
                levelImage.SetActive(true);
                Invoke("RestartLevel", 3f); //TODO: Change to load next level
            }
            else
            {
                GameManager.instance.DisplayShortInfoText("You need two keys to open the door");
            }

        }

        if (colInfo.transform.tag == "FallPlane")
        {
            GameManager.instance.GameOver();
        }

        if (colInfo.transform.tag == "Item")
        {
            if (itemCollectAvailable)
            {
                itemCollectAvailable = false;
                GameManager.instance.IncreaseItemCount();
                colInfo.gameObject.SetActive(false);
                Invoke("ReenableCollectItem", 0.2f);
            }

        }

        if (colInfo.transform.tag == "Key")
        {
            if (itemCollectAvailable)
            {
                itemCollectAvailable = false;
                GameManager.instance.IncreaseKeyCount();
                colInfo.gameObject.SetActive(false);
                Invoke("ReenableCollectItem", 0.2f);

            }

        }

        if (colInfo.transform.tag == "Tile" || colInfo.transform.tag == "SwitchingTile")
        {
            GroundCollision gCol = colInfo.gameObject.GetComponent<GroundCollision>();
            int tileColor = gCol.blockType;
            //Debug.Log("Player color at collision => " + currentState);
            //Debug.Log("Block color at Collision => " + tileColor);

            switch (tileColor)
            {
                case 0:
                    currentState = tileColor;
                    break;

                case 1:
                    currentState = tileColor;
                    break;

                case 2:
                    DamagePlayer(5);
                    break;

                case 10:
                    GameManager.instance.GameOver();
                    break;
            }

            //if (tileColor==0 || tileColor==1)
            //{
            //    if (currentState != tileColor)
            //        currentState = tileColor;
            //}
            //else
            //{
            //    playerHealth--;
            //}

            //Debug.Log("Player color after collision => " + currentState);

        }

        healthText.text = "Health: " + playerHealth.ToString();
        jump = false; /// player is grounded after full jump

    }

    private void CheckIfDead()
    {
        if (playerHealth <= 0)
        {
            enabled = false;
            GameManager.instance.GameOver();

        }
    }


    private void RestartLevel()
    {
        GameManager.instance.GameOver();
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
        CheckIfDead();
        damaged = true;

    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
        if (grounded)
        {
            timeInMidair = 0f;
        }

    }

    private void ReenableCollectItem()
    {
        itemCollectAvailable = true;
    }
}
