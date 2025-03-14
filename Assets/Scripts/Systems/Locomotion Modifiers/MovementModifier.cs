using UnityEngine;

public class MovementModifier : MonoBehaviour
{
    [SerializeField, Tooltip("Default Movement Modifier")]
    private float defaultMovementModifier = 1f;
    
    public float movementModifier;

    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.ModifyMovement += ModifyMovement;
            controller.ResetAction += ResetMovementModifier;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.ModifyMovement -= ModifyMovement;
            controller.ResetAction -= ResetMovementModifier;
        }
    }

    protected void ModifyMovement(float val)
    {
        movementModifier = val;
    }

    protected void ResetMovementModifier()
    {
        controller.ModifyMovement(defaultMovementModifier);
    }
    
}