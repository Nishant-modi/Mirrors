using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public bool isGrounded = false;
    int dir;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        dir = controller.dir;
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2) * (-dir);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex * (-dir);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * timeToJumpApex) * (-dir);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

        
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            isGrounded = true;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
            
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            
        }

        


        if(dir == -1)
        {
            if (jumpBufferCounter>0 && coyoteTimeCounter > 0)
            {
                velocity.y = maxJumpVelocity;
                isGrounded = false;
                jumpBufferCounter = 0;
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                if(velocity.y > minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
                coyoteTimeCounter = 0;
            }
        }
        else if (dir == 1)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                velocity.y = maxJumpVelocity;
                isGrounded = false;
                jumpBufferCounter = 0;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (-velocity.y > -minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
                coyoteTimeCounter = 0;
            }
        }
        

        float targetVelocityX = input.x * moveSpeed;
        if(dir == -1)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        else if(dir == 1)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.above) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
