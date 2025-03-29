using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform testTarget;

    [SerializeField] private PlayerLookData lookData;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] protected float lookLimitter;

    private float horizontalRot;
    private float horizontalRotCam;
    private float rotation;
    private float verticalRot;

    private float lookModifier = 1f;
    private float lockOutCounter = 0f;

    //Input Variables
    protected Vector2 lookIN;
    protected bool notAttacking;

    Quaternion lookRotation;

    private void Start() 
    {
        verticalRot = cameraTarget.eulerAngles.x;
        horizontalRot = transform.rotation.y;
    }

    private void LockOutCheck()
    {
        float magnitude = lookIN.normalized.magnitude;

        if(magnitude == 0f)
        {
            lockOutCounter = 0f;
            return;
        }

        lockOutCounter += lookIN.normalized.magnitude;

        if(lockOutCounter >= lookData.lockOutThreshold)
        {
            testTarget = null;
            lockOutCounter = 0f;
            return;
        }
    }

    private void Update() 
    {
        if(testTarget != null)
        {
            LockOutCheck();
            HandleLookAtTarget(); 
            return;
        }

        VerticalLook();
        HorizontalLook();

        // Update camera target rotation
        cameraTarget.eulerAngles = new Vector3(verticalRot, horizontalRot, 0f);
    }

    void VerticalLook()
    {
        // Calculate and clamp vertical rotation for the camera target
        verticalRot = Mathf.Clamp(
            verticalRot - (lookIN.y * lookData.aimSensitivity * lookModifier),
            lookData.minRotation,
            lookData.maxRotation
        );
    }

    void HorizontalLook()
    {
        horizontalRot += lookIN.x * lookData.aimSensitivity * lookModifier;
        rotation = Mathf.Lerp(rotation, horizontalRot, turnSmoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }

    void HandleLookAtTarget()
    {
        Vector3 direction = testTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation.x = rotation.z = 0f;

        lookRotation = Quaternion.Lerp(lookRotation, rotation, turnSmoothTime * Time.deltaTime);
        transform.rotation = lookRotation;
    }

    protected void ModifyLook(float val)
    {
        lookModifier = val;
    }
}