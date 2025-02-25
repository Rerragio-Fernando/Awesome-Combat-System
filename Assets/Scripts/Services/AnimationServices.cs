using UnityEngine;
/// <summary>
/// This class is a static service class which is used to 
/// handle the animations being played
/// </summary>
public static class AnimationServices
{
    public static void SetBlendTreeParameter(Animator animator, string parameterName, float value, float smoothTime = 0.1f)
    {
        float val = animator.GetFloat(parameterName);
        val = Mathf.Lerp(val, value, smoothTime * Time.deltaTime);
        animator.SetFloat(parameterName, val);
    }

    public static void PlayAnimation(Animator anim, string animation, float crossFade = 0.2f)
    {
        anim.CrossFade(animation, crossFade);
    }
    
    // this.animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName")
}