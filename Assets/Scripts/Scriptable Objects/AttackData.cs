using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data", menuName = "Scriptable Objects/Player Skills Data/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Attack Properties")]
    [Tooltip("Time window in which when attacked it will continue the combo")]
    public float nextMeleeAttackTimeWindow;

    [Header("Animator Related Properties")]
    [Tooltip("Name given in the Animator")]
    public string animationStateName;

    [Tooltip("Cross-Fade duration")]
    public float crossFade;
}