using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove  : MonoBehaviour
{
    float playerHeight = 2f;

    

    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;
    
    [SerializeField] WallRun WallRun;
    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float normalFov = 80;
    [SerializeField] float sprintFov = 100;
    [SerializeField] Camera cam;
    [SerializeField] float fovChangeTime = 20f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    [Header("Crouching")]
    private bool isCrouching = false;
    private bool canSlide = false;
    private float slideCooldown = 3f;
    private float nextSlideTime;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();
        if(Time.time > nextSlideTime && rb.velocity.magnitude>13)
        {
            canSlide = true;
        }
        else
        {
            canSlide = false;
        }
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if(Input.GetKey(crouchKey) && isGrounded)
        {
            transform.localScale = new Vector3(1f,0.5f,1f);
            if(isCrouching == false && canSlide == true)
            {
                Slide();
                nextSlideTime = Time.time + slideCooldown; 
                canSlide = false;
            }
            isCrouching = true;
        }
        if(isCrouching && Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(1f,1f,1f);
            isCrouching = false;
        }
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
    void Slide()
    {
        rb.AddForce(moveDirection.normalized * walkSpeed * movementMultiplier, ForceMode.Impulse);
    }
    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
    
        if(WallRun.isWallRunning)
        {
            if(Input.GetKey(sprintKey))
            {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
        }
        else if (Input.GetKey(sprintKey) && isGrounded)
        {
           
            if(!isCrouching)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,sprintFov,fovChangeTime);
            }
            else //crouch running
            {
                moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,normalFov,fovChangeTime);
            }
        }
        else
        {
            if(isCrouching && isGrounded) // if crouch walking
            {
                moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            }
            else if(isGrounded) // if just walking
            {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
            if(Input.GetKey(sprintKey) && !isCrouching)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,sprintFov,fovChangeTime);
            }
            else 
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,normalFov,fovChangeTime);
            }
            
        }
    }

    void ControlDrag()
    {
     
      if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
