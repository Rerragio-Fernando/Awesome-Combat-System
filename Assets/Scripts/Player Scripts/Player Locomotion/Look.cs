using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform playerGraphics;
    [SerializeField] private PlayerLookData lookData;
    
    [SerializeField] private LayerMask lookAtLayer;
    [SerializeField] private float rayCastDistance;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] protected float lookLimitter;

    private float horizontalRot;
    private float horizontalRotCam;
    private float rotation;
    private float verticalRot;
    
    private Camera playerCamera;

    private float lookModifier = 1f;
    private float lockOutCounter = 0f;

    //Input Variables
    protected Vector2 lookIN;
    protected Vector3 aimPoint;

    Quaternion lookRotation;

    private void Start() 
    {
        verticalRot = cameraTarget.eulerAngles.x;
        horizontalRot = transform.rotation.y;
        
        playerCamera = Camera.main;
    }

    private void Update()
    {
        Raycast();
        
        VerticalLook();
        HorizontalLook();

        RotatePlayerGraphics();

        // Update camera target rotation
        cameraTarget.eulerAngles = new Vector3(verticalRot, horizontalRot, 0f);
    }

    void Raycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Use the aim layer mask in the raycast
        if (Physics.Raycast(ray, out hit, rayCastDistance, lookAtLayer)) // If we hit something
        {
            aimPoint = hit.point; // Set aim point to the hit position
        }
    }

    void VerticalLook()
    {
        // Calculate and clamp vertical rotation for the camera target
        verticalRot = Mathf.Clamp(
            verticalRot - (lookIN.y * lookData.aimSensitivity * 1f),
            lookData.minRotation,
            lookData.maxRotation
        );
    }

    void HorizontalLook()
    {
        horizontalRot += lookIN.x * lookData.aimSensitivity * 1f;
        rotation = Mathf.Lerp(rotation, horizontalRot, turnSmoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }

    private void RotatePlayerGraphics()
    {
        Quaternion targetRotation = Quaternion.LookRotation(aimPoint - playerGraphics.position);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        playerGraphics.rotation = Quaternion.Slerp(playerGraphics.rotation, targetRotation, Time.deltaTime * 10f);
    }

    protected void ModifyLook(float val)
    {
        lookModifier = val;
    }
}