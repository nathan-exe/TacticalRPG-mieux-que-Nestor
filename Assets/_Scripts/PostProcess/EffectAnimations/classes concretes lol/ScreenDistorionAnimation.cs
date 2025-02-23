using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class ScreenDistortionAnimation : PostProcessEffectAnimation<LensDistortion>
{
    public float offset;
    float startValue;

    float AnimStartValue;

    public AnimationCurve AlphaCurve01;
    public override void OnDestroy()
    {
        try { _component.intensity.value = startValue; } catch { };
    }

    protected override void ApplyEffect(LensDistortion component, float alpha)
    {
        component.intensity.value = Mathf.Lerp(Mathf.Lerp(AnimStartValue, startValue, alpha), startValue + offset, AlphaCurve01.Evaluate(alpha));
    }

    protected override void OnBeforePlay()
    {
        if(_component != null)
        AnimStartValue = _component.intensity.value;
    }

    protected override void OnSetUp()
    {
        if (_component != null)
        startValue = _component.intensity.value;
    }

}