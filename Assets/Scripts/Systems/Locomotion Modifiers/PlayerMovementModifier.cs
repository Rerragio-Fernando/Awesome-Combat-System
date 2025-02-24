using UnityEngine;

public class PlayerMovementModifier : MovementModifier
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            // controller.BasicAttack += ModifyMovement;
            controller.ResetAction += ResetAnimationState;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            // controller.BasicAttack -= ModifyMovement;
            controller.ResetAction -= ResetAnimationState;
        }
    }
}