using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    //Managing speed.
    public float moveSpeed;
    public float speedMultiplier;
    public float speedUpCondition;
    private float speedUpConditionCount;

    //Managing jump.
    public float jumpForce;
    public float hangTime;
    private float hangTimeCounter;
    public bool jumpState;

    //Managing ground state.
    public bool groundState;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    //Managing if player is alive.
    public bool liveState;
    public int health;

    //Managing power ups.
    public PowerUps powerUp;
    public bool powerUpState;
    public bool tornadoState;
    public bool doublePointsState;  

    //Managing sounds.
    public AudioSource background;
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //Managing animation and physics.
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    //Managing menus.
    public DeathMenu deathMenu;
    public PauseMenu pauseMenu;
    public bool pauseState;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        speedUpConditionCount = speedUpCondition;

        hangTimeCounter = hangTime;

        background.Play();

        deathMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Moves player
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        
        //Check if player is touching ground.
        groundState = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Speed up the game after an amount of distance passed.
        if(transform.position.x > speedUpConditionCount)
        {
            speedUpConditionCount += speedUpCondition;
            speedUpCondition *= speedMultiplier;

            moveSpeed *= speedMultiplier;
        }

        //Allow player to jump with space bar or mouse click.
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (groundState && liveState)
            {
                if(!pauseState)
                {
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                    jumpState = true;
                    jumpSound.Play();
                }
                
            }
        }

        //Allow player to jump high if space bar or mouse click is longer.
        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && jumpState)
        {
            if(hangTimeCounter > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                hangTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            hangTimeCounter = 0;
            jumpState = false;
        }

        //Reset hang time.
        if(groundState == true)
        {
            hangTimeCounter = hangTime;
        }
        
        //Pauses game.
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!pauseState)
            {
                pauseState = true;
                pauseMenu.PauseGame();
                pauseMenu.gameObject.SetActive(true);
            }
            else
            {
                pauseState = false;
                pauseMenu.ResumeGame();
                pauseMenu.gameObject.SetActive(false);
            }
        }

        //Kill player when health is 0.
        if(health == 0)
        {
            deadState();
        }

        //Allowing players to jump as many times as they like in tornado state.
        if(tornadoState)
        {
            groundState = true;
            powerUp.tornadoDuration -= Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                if (groundState && liveState)
                {
                    if (!pauseState)
                    {
                        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                        jumpState = true;
                        jumpSound.Play();
                    }
                }
            }

            if (powerUp.tornadoDuration <= 0)
            {
                powerUp.tornadoDuration = powerUp.startPowerUpDuration;
                tornadoState = false;
                powerUpState = false;
                groundState = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            }
        }

        //Sending information to animator.
        myAnimator.SetFloat("moveSpeed", myRigidBody.velocity.x);
        myAnimator.SetBool("groundState", groundState);
        myAnimator.SetBool("liveState", liveState);
        myAnimator.SetBool("tornadoState", tornadoState);
        myAnimator.SetBool("doublePointsState", doublePointsState);
	}

    //What happens when player dies.
    public void deadState()
    {
        liveState = false;
        health = 0;
        deathSound.Play();
        background.Stop();
        deathMenu.gameObject.SetActive(true);
    }

    //When player falls.
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "KillBox")
        {
            deadState();
        }
    }
    
}
