using UnityEngine;

public class TargettingSystem : MonoBehaviour
{
    [SerializeField, Tooltip("Enemy Targetting Raycast Transform")]
    private Transform raycastTrans;

    [SerializeField, Tooltip("Raycast Range")]
    private float raycastRange;

    protected Vector2 movementIN;
    private Vector3 worldMoveDirection;

    private void Update() {

        Vector3 moveDirection = new Vector3(movementIN.x, 0f, movementIN.y).normalized;

        // Check if there is movement input
        if (moveDirection.magnitude >= 0.5f)
        {
            // Convert to world direction relative to character's forward direction
            worldMoveDirection = transform.TransformDirection(moveDirection);
        }

        // Draw the ray in the Scene view
        Debug.DrawRay(raycastTrans.position, worldMoveDirection * raycastRange, Color.red);
    }
}