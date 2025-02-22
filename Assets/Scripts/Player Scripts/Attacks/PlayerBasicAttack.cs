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
            controller.ResetAction += BackToEmpty;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.BasicAttack -= Attack;
            controller.ResetAction -= BackToEmpty;
        }
    }
}