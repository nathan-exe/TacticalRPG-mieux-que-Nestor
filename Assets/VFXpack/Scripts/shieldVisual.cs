using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

/// <summary>
/// This script contains some functions meant to easily control the shield VFX's visuals and animations,but does not contain any real gameplay logic. The demo scene contains an example script with some fake gameplay logic.
/// </summary>
public class shieldVisual : MonoBehaviour
{

    [Range(0, 1f)] 
    [SerializeField] 
    float _healthValue = 1;
    public float HealthValue => _healthValue;

    VisualEffect _vfx;

    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        _vfx = GetComponent<VisualEffect>();
        SetHealthValue(HealthValue);
    }


    /// <param name="newValue"> The new Health Value, between 0 and 1</param>
    /// <param name="playHitAnimation"> Should the hit animation also be played or not?</param>
    public void SetHealthValue(float newValue, bool playHitAnimation = false)
    {
        _healthValue = Mathf.Clamp01( newValue);
        _vfx.SetFloat("Health",HealthValue);
        if (playHitAnimation) PlayHitAnimation();
    }

    public void PlayBreakAnimation()
    {
        killMainParticle();
        _vfx.SendEvent("OnBreak");
    }

    public async void DeactivateShield()
    {
        _vfx.SetFloat("LastActivationTime", Time.time);
        _vfx.SetBool("DisableOnAnimationActivation", true);
        _vfx.SendEvent("OnDisable");

        await Task.Delay((int)(_vfx.GetFloat("ActivationAnimationDuration")*1000));
        if(_vfx.GetBool("DisableOnAnimationActivation"))killMainParticle();
    }

    public void ActivateShield()
    {
        _vfx.SetBool("killLoopingParticle", false);
        _vfx.SetFloat("LastActivationTime", Time.time);
        _vfx.SetBool("DisableOnAnimationActivation", false);
        _vfx.SendEvent("OnActivate");
    }

    public void PlayHitAnimation()
    {
        _vfx.SetFloat("LastHitTime", Time.time);
    }


    private void killMainParticle() => _vfx.SetBool("killLoopingParticle", true);

}
