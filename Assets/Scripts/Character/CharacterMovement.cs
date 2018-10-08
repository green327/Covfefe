using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    //check if player is sliding
    bool sliding = false;

    //store the slide time
    float slideTimer = 0f;    
    
    //set the maximum time to slide
    public float maxSlideTime = 1.5f;

    public float speed = 6f;
	public float sprintSpeed = 8f;
	public float jumpSpeed = 10f;
	public float gravity = 20f;
	private Vector2 moveDirection = Vector2.zero;
	public bool isJumping = false;
    public bool canDoubleJump = false;
    public int jumpCount = 0;
    private Rigidbody2D rb2d;


    [HideInInspector]
    public bool isFacingLeft = false;
	public bool isMoving = false;
	public bool isSprinting = false;
	public double previousX;
	public double currentX;
    public bool isGrounded = true;

	/// sound
	private AudioSource source;
	public AudioClip jumpSound;
	public AudioClip spawnSound;

    public Animator anim;

    // Use this for initialization
    void Start () 
	{
        anim = gameObject.GetComponentInChildren<Animator>();

		source = GetComponent<AudioSource>();
		source.PlayOneShot(spawnSound);
        rb2d = GetComponent<Rigidbody2D>();
       // controller.height = 4.2f;
        
    }

    // Update is called once per frame
    void Update()
    { 
        previousX = System.Math.Round(this.gameObject.transform.position.x, 2);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isSprinting = false;
        }
        
        if (isGrounded == true)
        {
            moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            moveDirection = transform.TransformDirection(moveDirection);
            isJumping = false;
            //anim.SetBool("isJumping", isJumping);
            DBMovement();

            if (isSprinting)
            {
                moveDirection *= 0; //sprintSpeed;
            }

            if (!isSprinting)
            {
                moveDirection *= 0; //speed;
            }
        }

        if (Input.GetButton("Jump"))
        {
            if (isGrounded == true)
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
                //anim.SetBool("isJumping", isJumping);
                source.PlayOneShot(jumpSound);
                jumpCount = 1;
            }
            else if (canDoubleJump)
            {
                if (jumpCount < 2)
                {
                    jumpCount = 2;
                    moveDirection.y = jumpSpeed;
                    isJumping = true;
                    source.PlayOneShot(jumpSound);
                }
                else
                {
                    jumpCount = 0;
                }
            }
        }
        

        //slide input
        if(Input.GetButtonDown("Slide") && !sliding && !isJumping)
        {
            slideTimer = 0f;

            //HOOK UP AFTER ANIMATION
            //anim.SetBool("isSliding",true);
            //rb.velocity = Vector3.right * 10;
            //rb2d.AddForce(Vector2.right * 10);
            //controller.height = 2.0f;
            if(isFacingLeft == false)
            {
                rb2d.transform.Translate(Vector2.right * 1);

            }
            else
            rb2d.transform.Translate(Vector2.left * 1);

            sliding = true;

        }

        if(sliding)
        {
            slideTimer += Time.deltaTime;
            if(slideTimer > maxSlideTime)
            {
                sliding = false;
                //HOOK UP AFTER ANIMATION
                //anim.SetBool("isSliding",false);

                //controller.height = 4.2f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection.x = Input.GetAxis("Horizontal") * ((isSprinting) ? sprintSpeed : speed);
        DBMovement();
        //controller.Move(moveDirection * Time.deltaTime);
        currentX = System.Math.Round(this.gameObject.transform.position.x, 2);

        //Checks for movemnt, used for animation
        if (previousX != currentX)
        {
            isMoving = true;
            //anim.SetBool("isMoving", isMoving);
        }

        else if (previousX == currentX)
        {
            isMoving = false;
            //anim.SetBool("isMoving", isMoving);
        }

        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFacingLeft", isFacingLeft);
        anim.SetBool("isMoving", isMoving);
    }

    void DBMovement()
	{
		if(moveDirection.x > 0)
		{
			isFacingLeft = false;
            //anim.SetBool("isFacingLeft", isFacingLeft);
        }
		else if(moveDirection.x < 0)
		{
			isFacingLeft = true;
            //anim.SetBool("isFacingLeft", isFacingLeft);
        }
	}

    void GroundCheck(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isGrounded = true;
        }
    }
}
