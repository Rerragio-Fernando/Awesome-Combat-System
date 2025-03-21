using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "Scriptable Objects/Player Skills Data/Stat Data")]
public class PlayerStatData : ScriptableObject
{
    [Tooltip("Player Max HP")]
    public float playerMaxHP = 100f;

    [Tooltip("Miss Probability Threshold"), Range(0f, 1f)]
    public float missThreshold;

    [Tooltip("Light Hit Probability Threshold"), Range(0f, 1f)]
    public float lightThreshold;

    [Tooltip("Strong Hit Probability Threshold"), Range(0f, 1f)]
    public float strongThreshold;

    [Tooltip("Critical Hit Probability Threshold"), Range(0f, 1f)]
    public float criticalThreshold;
}