using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "Scriptable Objects/Player Skills Data/Stat Data")]
public class PlayerStatData : ScriptableObject
{
    [Tooltip("Player Max HP")]
    public float playerMaxHP = 100f;

    [Header("Player Miss")]
    [Tooltip("Miss Probability Threshold"), Range(0f, 1f)]
    public float missThreshold;

    [Tooltip("Miss Magnitude")]
    public float missMagnitude = 0f;

    [Header("Player Light")]
    [Tooltip("Light Hit Probability Threshold"), Range(0f, 1f)]
    public float lightThreshold;

    [Tooltip("Light Hit Magnitude")]
    public float lightMagnitude = 0.5f;

    [Header("Player Strong")]
    [Tooltip("Strong Hit Probability Threshold"), Range(0f, 1f)]
    public float strongThreshold;

    [Tooltip("Strong Hit Magnitude")]
    public float strongMagnitude = 0.75f;

    [Header("Player Critical")]
    [Tooltip("Critical Hit Probability Threshold"), Range(0f, 1f)]
    public float criticalThreshold;

    [Tooltip("Critical Hit Magnitude")]
    public float criticalMagnitude = 1f;
}