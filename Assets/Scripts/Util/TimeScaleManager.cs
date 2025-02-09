using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    [SerializeField] private float _anticipationTimeScale;
    [SerializeField] private float _anticipationTimeDuration;

    private float _timeScale = 1f;
    private float _fixedDeltaTime;

    private void Start() 
    {
        _fixedDeltaTime = Time.fixedDeltaTime;
        PlayerEventSystem.CombatStateEvent += CombatStateReceiver;
    }

    void CombatStateReceiver(CombatState cs)
    {
        if(cs == CombatState.Anticipation)
            Time.timeScale = _anticipationTimeScale;
        else
            Time.timeScale = 1.0f;

        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
    }
}