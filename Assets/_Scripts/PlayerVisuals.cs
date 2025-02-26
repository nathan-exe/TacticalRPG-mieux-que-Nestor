using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class PlayerVisuals : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    [SerializeField] VisualEffect _walkVFX;
    bool _walking;

    [Header("rotation")]
    [SerializeField][Range(0,1f)] float _smoothFactor = .1f;
    [SerializeField] float _tiltIntensity = .1f;

    Vector3 _forward = Vector3.zero;

    private void Awake()
    {
        _walkVFX.Stop();
    }
    // Update is called once per frame
    async void Update()
    {
        Vector3 oldPose = transform.position;
        await UniTask.Yield();
        if (this == null) return;
        Vector3 vel = ( transform.position- oldPose)/Time.deltaTime;

        //rotation
        if (vel != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(vel.normalized + Vector3.down * vel.magnitude * _tiltIntensity, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Mathf.Pow(_smoothFactor, Time.deltaTime));
        }


        if (vel.sqrMagnitude > 0.2f)
        {
            if(!_walking) _walkVFX.Play();
            _walking = true;
        }
        else
        {
            if (_walking) _walkVFX.Stop();
            _walking = false;
        }
    }
}
