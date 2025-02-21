using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private PlayerLookData lookData;
    [SerializeField] private float turnSmoothTime;

    private float horizontalRot;
    private float rotation;
    private float verticalRot;

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
    }

    void VerticalLook()
    {
        // Calculate and clamp vertical rotation for the camera target
        verticalRot = Mathf.Clamp(
            verticalRot - (lookIN.y * lookData.aimSensitivity),
            lookData.minRotation,
            lookData.maxRotation
        );

        // Update camera target rotation
        cameraTarget.eulerAngles = new Vector3(verticalRot, horizontalRot, 0f);
    }

    void HorizontalLook()
    {
        horizontalRot += lookIN.x * lookData.aimSensitivity;
        rotation = Mathf.Lerp(rotation, horizontalRot, turnSmoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }
}