using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data", menuName = "Scriptable Objects/Player Skills Data/Attack Data")]
public class AttackData : ScriptableObject
{
    [Tooltip("Name given in the Animator")]
    public string animationStateName;
}