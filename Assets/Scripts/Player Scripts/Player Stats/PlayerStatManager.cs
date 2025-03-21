using System;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField, Tooltip("Attack Stats")]
    private PlayerStatData playerStatData;

    public Hit GetHitProbability()
    {
        float miss = UnityEngine.Random.Range(0f, playerStatData.missThreshold);
        float light = UnityEngine.Random.Range(0f, playerStatData.lightThreshold);
        float strong = UnityEngine.Random.Range(0f, playerStatData.strongThreshold);
        float critical = UnityEngine.Random.Range(0f, playerStatData.criticalThreshold);

        float maxValue = Math.Max(miss, Math.Max(light, Math.Max(strong, critical)));

        if(miss == maxValue)
        {
            return new Hit(HitLevel.MISS, 0f);
        }
        else if(light == maxValue)
        {
            return new Hit(HitLevel.LIGHT, maxValue);   
        }
        else if(strong == maxValue)
        {
            return new Hit(HitLevel.STRONG, maxValue);   
        }
        else
        {
            return new Hit(HitLevel.CRITICAL, maxValue);   
        }
    }
}