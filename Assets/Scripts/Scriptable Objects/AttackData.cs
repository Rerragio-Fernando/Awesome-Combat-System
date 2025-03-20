using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data", menuName = "Scriptable Objects/Player Skills Data/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Attack Properties")]
    [Tooltip("Hit Force")]
    public float hitForce;

    [Tooltip("Forward Step Force")]
    public float forwardStep;

    [Tooltip("Time window in which when attacked it will continue the combo")]
    public float nextMeleeAttackTimeWindow;

    [Tooltip("Radius of Check-Sphere")]
    public float checkSphereRadius = 0.5f;

    [Tooltip("Attack Hit Layer")]
    public LayerMask hitLayerMask;

    [Tooltip("Movement Modifier")]
    public float movementModifier;

    [Tooltip("Look Modifier")]
    public float lookModifier;

    [Header("Animator Related Properties")]
    [Tooltip("Name given in the Animator")]
    public string animationStateName;

    [Tooltip("Cross-Fade duration")]
    public float crossFade;

    [Header("VFX Properties")]
    public GameObject hitFx;
}