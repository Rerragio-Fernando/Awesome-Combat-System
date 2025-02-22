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

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Guarding(bool val)
    {
        isGuarding = val;
    }

    protected virtual void Update() 
    {
        Grounded();
        PlayerMovementFunction();

        cont.Move(velocity * Time.deltaTime);
    }

    void Grounded()
    {
        isGrounded = Physics.CheckSphere(groundTrans.position, distanceToGround, groundLayer);
    }
    
    void PlayerMovementFunction()
    {
        if(!isGuarding)
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
            if(movDir.magnitude > 0.25f && isGrounded)
            {
                if(sprintIN && (movDir.z > 0f && (movDir.x < 0.25f && movDir.x > -0.25f)))
                {
                    moveSpeed = movementData.runSpeed;
                }
                else
                {
                    sprintIN = false;
                    moveSpeed = movementData.walkSpeed;
                }
                Vector3 moveDirection = Quaternion.Euler(0f, targAngle, 0f) * Vector3.forward;
                Vector3 adjustedVelocity = moveDirection.normalized * moveSpeed;
                velocity = new Vector3(adjustedVelocity.x, velocity.y, adjustedVelocity.z);
            }
            else
            {
                sprintIN = false;
                ApplyFriction();
            }
        }
    }

    void ApplyFriction()
    {
        float friction = isGrounded ? playerFriction : 0.05f;
        velocity = Vector3.Lerp(velocity, new Vector3(0f, velocity.y, 0f), friction * Time.deltaTime);
    }
}