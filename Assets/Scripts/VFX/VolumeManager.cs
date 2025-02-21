using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    [Header("Anticipation Volume")]
    [SerializeField] private Volume _anticipationVolume;
    [SerializeField] private float _anticipationVolumeSmoothTime;

    // private bool _anticipationTrigger = false;

    // private void Start() 
    // {
    //     PlayerEventSystem.CombatStateEvent += CombatStateReceiver;
    // }

    // private void Update() 
    // {
    //     if(_anticipationTrigger)
    //         _anticipationVolume.weight = Mathf.Lerp(_anticipationVolume.weight, 1f, _anticipationVolumeSmoothTime * Time.deltaTime);
    //     else
    //         _anticipationVolume.weight = Mathf.Lerp(_anticipationVolume.weight, 0f, _anticipationVolumeSmoothTime * Time.deltaTime);
    // }

    // void CombatStateReceiver(CombatState cs)
    // {
    //     if(cs == CombatState.Anticipation)
    //         _anticipationTrigger = true;
    //     else
    //         _anticipationTrigger = false;
    // }

    // private void AnticipationVolumeOn()
    // {
    //     StartCoroutine(AnticipationVolume());
    // }
    
    // IEnumerator AnticipationVolume()
    // {
    //     _anticipationTrigger = true;

    //     yield return new WaitForSeconds(.25f);

    //     _anticipationTrigger = false;
    // }
}