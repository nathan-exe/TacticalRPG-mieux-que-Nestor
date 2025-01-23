using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] AnimationCurve _timeDilatationIntensityCurve;
    [SerializeField] float TimeDilatationFactor = 0.2f;
    [SerializeField] float TimeDilatationDuration = 0.5f;

    float animStartValue;
    Coroutine DilatationCoroutine;

    //singleton
    public static TimeManager instance { get; private set ; }

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }


    IEnumerator DilateTime(float duration)
    {
        animStartValue = Time.timeScale;

        float endTime = Time.realtimeSinceStartup + duration;
        while (Time.realtimeSinceStartup < endTime)
        {
            //calculer l'alpha
            float alpha = 1 - (endTime - Time.realtimeSinceStartup) / duration;

            //lerp
            float baseValue = Mathf.Lerp(animStartValue, 1, alpha);
            Time.timeScale = Mathf.Lerp(baseValue, baseValue * TimeDilatationFactor, _timeDilatationIntensityCurve.Evaluate(alpha));

            //attendre la frame suivante
            yield return null;
        }

        Time.timeScale = 1;
    }

    IEnumerator C_StopTime(float transitionDuration)
    {
        animStartValue = Time.timeScale;
        float endTime = Time.realtimeSinceStartup + transitionDuration;
        while (Time.realtimeSinceStartup < endTime)
        {
            //calculer l'alpha
            float alpha = 1f - (endTime - Time.realtimeSinceStartup) / transitionDuration;

            //lerp
            Time.timeScale = Mathf.Lerp(animStartValue, 0, alpha);

            //attendre la frame suivante
            yield return null;
        }

        Time.timeScale = 0;
    }

    public void StopTime(float t)
    {
        if (DilatationCoroutine != null) StopCoroutine(DilatationCoroutine);
        StartCoroutine(C_StopTime(t));
    }


    public void PlayTimeDilatationAnimation()
    {
        if (DilatationCoroutine != null) StopCoroutine(DilatationCoroutine);
        DilatationCoroutine = StartCoroutine(DilateTime(TimeDilatationDuration));
    }
}
