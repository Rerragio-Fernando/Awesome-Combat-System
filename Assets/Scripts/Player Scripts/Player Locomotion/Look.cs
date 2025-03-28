using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private PlayerLookData lookData;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] protected float lookLimitter;

    private float horizontalRot;
    private float horizontalRotCam;
    private float rotation;
    private float verticalRot;

    private float lookModifier = 1f;

    //Input Variables
    protected Vector2 lookIN;
    protected bool notAttacking;

    private void Start() 
    {
        verticalRot = cameraTarget.eulerAngles.x;
        horizontalRot = transform.rotation.y;
    }

    private void Update() 
    {
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

    protected void ModifyLook(float val)
    {
        lookModifier = val;
    }
}