using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public CharacterMovement2D controller;

    public float gravity = 20f;
    public float runSpeed = 20f;

    public float sprintSpeed = 40f;
    private Vector2 moveDirection = Vector2.zero;

    float horizontalMove = 0f;

    bool isJumping = false;
    public bool isSprinting = false;
    public bool isFacingLeft = false;
    public bool isGrounded = true;
    public double previousX;
    public double currentX;

    void Start()
    {

    }


	// Update is called once per frame
	void Update ()
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

        if (isSprinting == true)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
        
            horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
        }

      
       
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }


  
    

    
    }

    void FixedUpdate()
    {
        //Move our Character (movement, counch, jump)
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}
