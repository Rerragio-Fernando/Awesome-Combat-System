using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrompt : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrompt;

    private void Start() 
    {
        _playerPrompt.SetActive(false);
        PlayerEventSystem.CombatStateEvent += CombatStateReceiver;
    }

    void CombatStateReceiver(CombatState cs)
    {
        if(cs == CombatState.Attacking)
            _playerPrompt.SetActive(true);
        else
            _playerPrompt.SetActive(false);
    }
}