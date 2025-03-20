using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Data", menuName = "Scriptable Objects/Data/Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [Tooltip("Player Mass")]
    public float playerMass;

    [Tooltip("Player Walk Speed")]
    public float walkSpeed;

    [Tooltip("Player Run Speed")]
    public float runSpeed;
}