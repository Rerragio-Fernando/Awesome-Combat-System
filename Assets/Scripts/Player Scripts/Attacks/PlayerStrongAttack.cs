using UnityEngine;

public class PlayerStrongAttack : MeleeAttack
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.StrongAttack += Attack;
            controller.ResetAction += BackToEmpty;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.StrongAttack -= Attack;
            controller.ResetAction -= BackToEmpty;
        }
    }
}