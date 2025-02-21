using System;
using UnityEngine;

public class MovementAnimator : Movement
{
    [Header("Animator Parameters")]
    [SerializeField, Tooltip("Name of the animator parameter which handles movement")]
    private string movementParameterName;

    [SerializeField, Tooltip("Name of the animator parameter which handles X Directional movement")]
    private string directionXParameterName;

    [SerializeField, Tooltip("Name of the animator parameter which handles Y Directional movement")]
    private string directionYParameterName;

    [SerializeField, Tooltip("Lerper float value")]
    private float smoothTimeLerp;

    protected override void Update(){
        base.Update();
        UpdateAnimatorValues();
    }

    /// <summary>
    /// Updates the blend tree values in animator
    /// </summary>
    private void UpdateAnimatorValues(){
        //Movement Amount
        float movementVal = (movementIN.magnitude >= 0.1f && sprintIN) ? 2f : (movementIN.magnitude >= 0.1f) ? 1f : 0f;
        AnimationServices.SetBlendTreeParameter(anim, movementParameterName, movementVal, smoothTimeLerp);

        //Directional Amount
        AnimationServices.SetBlendTreeParameter(anim, directionXParameterName, movementIN.x, smoothTimeLerp);
        AnimationServices.SetBlendTreeParameter(anim, directionYParameterName, movementIN.y, smoothTimeLerp);
    }
}