using UnityEngine;

public class CombatAnimation : MonoBehaviour
{
    private string emptyAnimationStateName = "Empty State";

    protected Animator anim;
    protected float crossFadeHolder;

    protected const float defaultCrossFade = 0.2f;

    protected virtual void Start() 
    {
        anim = GetComponentInParent<Animator>();

        crossFadeHolder = defaultCrossFade;
    }

    protected virtual void ResetAnimationState()
    {
        if(anim == null) return;
        
        AnimationServices.PlayAnimation(anim, emptyAnimationStateName, crossFadeHolder);
    }
}