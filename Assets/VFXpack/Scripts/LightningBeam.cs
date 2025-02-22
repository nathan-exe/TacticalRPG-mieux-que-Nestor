using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace SimpleVFXs
{

    public class LightningBeam : MonoBehaviour
    {


        [Header("References")]
        [SerializeField] private LineRenderer _lineRenderer;
        [Tooltip("The transform used for <b>PlayAnimationTowardTargetTransform()</b>")]
        [SerializeField] private Transform _targetTransform;
        [Tooltip("Can be left to None")]
        [SerializeField] private VisualEffect _optionalHitVFX;

        [Header("Line Shape")]
        [Tooltip("number of line points per unit")]
        [SerializeField][Range(.1f, 10)] private float _resolution = 2f;
        [SerializeField] private float _height = 1;
        private const int _maxPointCount = 100; //to prevent having too many points and potentially crashing the game in case of user mistake

        [Header("Animation")]
        [Tooltip("Wether the gameObject should Destroy itself after the animation is complete")]
        [SerializeField] private bool _autoDestroy = false;
        [SerializeField] private float _animationDuration = 1;
        [SerializeField] private float _animationDelay = 0;
        [SerializeField] private AnimationCurve _intensityOverLife;
        [SerializeField] private float _randomness = 1;

        private void OnValidate()
        {
            if (_lineRenderer == null) _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            MaterialPropertyBlock b = new();
            b.SetFloat("_AlphaMultiplier", 0f);
            _lineRenderer.SetPropertyBlock(b, 0);
        }

        private void ComputeLine(Vector3 targetEndPosition)
        {
            Vector3 RandomOffset = Random.insideUnitSphere * _randomness;
            float distance = Vector3.Distance(transform.position, targetEndPosition);
            _lineRenderer.positionCount = (int)(distance * _resolution);

            if (_lineRenderer.positionCount > _maxPointCount)
            {
                _lineRenderer.positionCount = _maxPointCount;
                Debug.LogWarning($"It seems that the target position of the lightning strike effect is very far away from the source GameObject ({gameObject.name}) or that the resolution was set way too high. the lineRenderer's position count has been Clamped to {_maxPointCount}. Consider lowering the resolution when using this effect over long distances, or using another kind of effect entirely. ");
            }

            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                float alpha = (float)i / (float)(_lineRenderer.positionCount - 1);
                Vector3 targetPosition = Vector3.Lerp(transform.position, targetEndPosition, alpha);
                targetPosition += (Vector3.up + RandomOffset) * (alpha) * (alpha - 1) * -1f * distance / 2 * _height;

                _lineRenderer.SetPosition(i, targetPosition);
            }

            if (_optionalHitVFX != null)
            {
                _optionalHitVFX.transform.forward = -(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1) - _lineRenderer.GetPosition(_lineRenderer.positionCount - 2));
                _optionalHitVFX.transform.position = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
            }

        }

        

        /// <summary>
        /// Plays the lightning strike animation toward the target transform
        /// </summary>
        public void PlayAnimationTowardTargetTransform()
        {
            ComputeLine(_targetTransform.position);

            if (_optionalHitVFX != null) _optionalHitVFX.Play();

            StartCoroutine(PlayAnimation());
        }

        /// <summary>
        /// Plays the lightning strike animation toward the given position
        /// </summary>
        /// <param name="TargetPosition"></param>
        public void PlayAnimationTowardPosition(Vector3 TargetPosition)
        {
            ComputeLine(TargetPosition);

            if (_optionalHitVFX != null) _optionalHitVFX.Play();

            StartCoroutine(PlayAnimation());
        }


        private IEnumerator PlayAnimation()
        {
            float endTime = Time.time + _animationDuration;
            MaterialPropertyBlock block = new();
            while (Time.time < endTime)
            {
                float alpha = 1f - (endTime - Time.time) / _animationDuration;
               
                block.SetFloat("_AlphaMultiplier", _intensityOverLife.Evaluate(alpha));
                _lineRenderer.SetPropertyBlock(block,0);

                yield return null;
            }

            block.SetFloat("_AlphaMultiplier", _intensityOverLife.Evaluate(1));
            _lineRenderer.SetPropertyBlock(block,0);

            if (_autoDestroy && Application.isPlaying) Destroy(gameObject);

        }
    }



}




//GUILayout.Box();
