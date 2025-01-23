using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class PostProcessController : MonoBehaviour
{
    [Header("references")]
    [SerializeField] VolumeProfile mVolumeProfile;
    [SerializeField] Animator _ImpactFrameVolumeAnimator;

    [Header("Effects")]
    public ExposureAnimation E_ExposureFlash = new();
    public ScreenDistortionAnimation E_ScreenDistortion = new();
    public FadingEffect FadeIn = new();
    public FadingEffect FadeOut = new();
    public ChromaticAberrationAnimation ChromaticAberrationFlash = new();

    //singleton
    public static PostProcessController instance { get;private set; }

    //animation management
    public Dictionary<Type, Coroutine> effectsCoroutines = new();

    public void PlayImpactFrameAnimation()
    {
        _ImpactFrameVolumeAnimator.SetTrigger("Play");
    }

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    void Start()
    {
        E_ExposureFlash.SetUp(mVolumeProfile);
        E_ScreenDistortion.SetUp(mVolumeProfile);
        FadeIn.SetUp(mVolumeProfile);
        FadeOut.SetUp(mVolumeProfile);
        ChromaticAberrationFlash.SetUp(mVolumeProfile);

        FadeIn.play();
    }

    private void OnDestroy()
    {
        E_ExposureFlash.OnDestroy();
        E_ScreenDistortion.OnDestroy();
        FadeIn.OnDestroy();
        FadeOut.OnDestroy();
        ChromaticAberrationFlash.OnDestroy();
    }
    

}