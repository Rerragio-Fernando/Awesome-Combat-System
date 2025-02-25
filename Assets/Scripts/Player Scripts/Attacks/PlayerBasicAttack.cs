using UnityEngine;

public class PlayerBasicAttack : MeleeAttack
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.BasicAttack += Attack;
            controller.ResetAction += ResetAnimationState;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.BasicAttack -= Attack;
            controller.ResetAction -= ResetAnimationState;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        controller.ModifyMovement(movementModifier);
        controller.ModifyLook(lookModifier);
    }
}