using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [Header("Player Properties")]
    [SerializeField] private PlayerMovementData movementData;
    [SerializeField] private float gravity;//
    [SerializeField] private float playerFriction;//
    [SerializeField] private float distanceToGround;//
    [SerializeField] private LayerMask groundLayer;//
    [SerializeField] private Transform groundTrans;

    private float targAngle;
    private float moveSpeed = 0f;
    private Vector3 movDir;
    private bool isGrounded;
    private bool isGuarding = false;
    private Vector3 velocity;
    private CharacterController cont;

    protected Animator anim;

    //Input Variables
    protected Vector2 movementIN;
    protected bool aimIN;
    protected bool sprintIN;
    
    protected bool isAttacking; 

    public float movementModifier = 1f;
    public float stepMultiplier = 1f;

    public Action<PlayerMovementData> ChangePlayerMovementEvent;

    //Interface Variables
    private bool overrideMovement;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable() {
        ChangePlayerMovementEvent += SetMovementAsset;
    }

    protected virtual void OnDisable() {
        ChangePlayerMovementEvent -= SetMovementAsset;
    }

    protected virtual void Update() 
    {
        Grounded();

        if(!isAttacking)
            PlayerMovementFunction();

        cont.Move(velocity * Time.deltaTime);
        ApplyFriction();
    }

    void Grounded()
    {
        isGrounded = Physics.CheckSphere(groundTrans.position, distanceToGround, groundLayer);
    }
    
    void PlayerMovementFunction()
    {
        // Handle vertical velocity
        if(!isGrounded)
        {
            velocity.y += -gravity * movementData.playerMass * Time.deltaTime;
        }
        else
        {
            velocity.y = -1f;
        }

        // Calculate movement direction and target angle
        movDir = new Vector3(movementIN.x, 0f, movementIN.y).normalized;
        targAngle = Mathf.Atan2(movDir.x, movDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        // Handle movement and animation states
        if(movDir.magnitude > 0.5f && isGrounded)
        {
            if(sprintIN)
            {
                moveSpeed = movementData.runSpeed;
            }
            else
            {
                moveSpeed = movementData.walkSpeed;
            }
            Vector3 moveDirection = Quaternion.Euler(0f, targAngle, 0f) * Vector3.forward;
            Vector3 adjustedVelocity = moveDirection.normalized * moveSpeed * movementModifier;
            velocity = new Vector3(adjustedVelocity.x, velocity.y, adjustedVelocity.z);
        }
        else
        {
            sprintIN = false;
        }
    }

    void ApplyFriction()
    {
        float friction = isGrounded ? playerFriction : 0.05f;
        velocity = Vector3.Lerp(velocity, new Vector3(0f, velocity.y, 0f), friction * Time.deltaTime);
    }

    protected void ModifyForwardStep(float value)
    {
        stepMultiplier = value;
    }

    public void ForwardStep()
    {
        velocity = transform.TransformDirection(Vector3.forward) * stepMultiplier; 
    }

    protected void ModifyMovement(float val)
    {
        movementModifier = val;
    }

    private void SetMovementAsset(PlayerMovementData asset)
    {
        movementData = asset;
    }
}