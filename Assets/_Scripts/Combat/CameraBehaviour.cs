using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controle la camera pendant les combats.
public class CameraBehaviour : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float _smoothTime = 1;

    [Header("Mouse Input")]
    [SerializeField] float _sensibility = 1;

    [Header("Target Tracking")]
    [SerializeField] float OptimalDistanceToTarget = 15;
    [SerializeField] bool _autoUnfollowTargetOnMouseDrag = false;
    [field: SerializeField] public Transform Target { get; private set; }
    [field: SerializeField] public bool FollowTarget { get; private set; } = true;


    Camera _cam;
    Vector3 _vel;
    Vector3 _worldMouvementInput;



    /// <summary>
    /// changes the Target Transform
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    /// <summary>
    /// Starts following the current Target Transform
    /// </summary>
    /// <param name="newTarget"></param>
    public void StartFollowingTarget(Transform newTarget = null)
    {
        if(newTarget!=null)Target = newTarget;
        FollowTarget = true;
    }

    /// <summary>
    /// Stops following the current Target Transform
    /// </summary>
    public void StopFollowingTarget()
    {
        FollowTarget = false;
    }


    private void Awake()
    {
        TryGetComponent(out _cam);
    }

    // Update is called once per frame
    void Update()
    {
        //mouse movement
        HandleMouseMovement();

        //target tracking
        Vector3 targetPosition = transform.position;
        FollowTarget &= Target != null;
        if (FollowTarget)
        {
            targetPosition = Target.transform.position - transform.forward * OptimalDistanceToTarget;
        }

        //smoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + _worldMouvementInput, ref _vel, _smoothTime);
    }




    async UniTask HandleMouseMovement()
    {
        Vector3 oldPosition = Input.mousePosition;
        await UniTask.Yield();

        if (Input.GetMouseButton(1))
        {
            if (_autoUnfollowTargetOnMouseDrag && _worldMouvementInput != Vector3.zero) StopFollowingTarget();

            Vector3 delta = Input.mousePosition - oldPosition;
            _worldMouvementInput = delta.x * _cam.transform.right + delta.y * _cam.transform.up;
            _worldMouvementInput.y = 0;
            _worldMouvementInput *= -_sensibility;
        }
        else
        {
            _worldMouvementInput = Vector3.zero;
        }
    }

}
