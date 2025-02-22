using UnityEngine;

public class GuardMechanic : CombatAnimation
{
    [SerializeField, Tooltip("Guard animation state name")]
    private string animationStateName;

    private Animator anim;

    private void Start() {
        anim = GetComponentInParent<Animator>();
    }

    protected void Guard(){
        Debug.Log($"Guarding");
        AnimationServices.PlayAnimation(anim, animationStateName);
    }
}