using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleVFXs
{
    public class LightAnimation : MonoBehaviour
    {
        [SerializeField] Light _Light;


        [Tooltip("Wether the gameObject should Destroy itself after the animation is complete")]
        [SerializeField] bool _AutoDestroy = true;
        [SerializeField] float _AnimationDuration = 1;
        [SerializeField] float _AnimationDelay = 0;
        [SerializeField] AnimationCurve _IntensityOverLife;

        [GradientUsage(false)]
        [SerializeField] Gradient _ColorOverLife;


        private IEnumerator PlayAnimation(bool PlayInReverse = false)
        {
            if (_AnimationDelay > 0) yield return new WaitForSeconds(_AnimationDelay);

            float startTime = Time.time + _AnimationDuration;
            float endTime = Time.time + _AnimationDuration;
            while (Time.time < endTime)
            {
                Assert.IsNotNull(_Light);
                float alpha = 1f - (endTime - Time.time) / _AnimationDuration;
                if (PlayInReverse) alpha = 1f - alpha;
                _Light.intensity = _IntensityOverLife.Evaluate(alpha);
                _Light.color = _ColorOverLife.Evaluate(alpha);
                yield return null;
            }

            float FinalAlpha = PlayInReverse ? 0 : 1;
            _Light.intensity = _IntensityOverLife.Evaluate(FinalAlpha);
            _Light.color = _ColorOverLife.Evaluate(FinalAlpha);

            if (_AutoDestroy) Destroy(gameObject);
        }

        public void Play() {StopCoroutine("PlayAnimation"); StartCoroutine(PlayAnimation(false)); }
        public void PlayInReverse() {StopCoroutine("PlayAnimation"); StartCoroutine(PlayAnimation(true)); }

        public void Stop()
        {
            StopCoroutine("PlayAnimation");
        }

        private void OnValidate()
        {
            if (!TryGetComponent<Light>(out _Light))
            {
                Debug.LogWarning("No light is attached to this gameObject.No Animation will play.");
                if (_IntensityOverLife.keys[_IntensityOverLife.keys.Length - 1].time > 1) Debug.LogWarning("AnimationCurve \"IntensityOverLife\" will only be evaluated between Time = 0 and Time = 1.");
            }
        }


    }
}