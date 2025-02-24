using UnityEngine;

public class MovementModifier : MonoBehaviour
{
    private float movementModifier;

    protected void ModifyMovement(float val)
    {
        movementModifier = val;
    }

    protected void ResetMovementModifier()
    {
        movementModifier = 1f;
    }
    
}