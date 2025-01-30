using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controle la camera pendant les combats.
public class CombatCameraBehaviour : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField] float _sensibility = 1;
    [SerializeField] float _smoothTime = 1;

    Camera _cam;
    Vector3 _vel;
    Vector3 _worldMouvementInput;

    private void Awake()
    {
        TryGetComponent(out _cam);
    }

    // Update is called once per frame
    void Update()
    {
        HandleScrolling();

        transform.position = Vector3.SmoothDamp(transform.position, transform.position + _worldMouvementInput, ref _vel, _smoothTime);
    }

    async UniTask HandleScrolling()
    {
        Vector3 oldPosition = Input.mousePosition;
        await UniTask.Yield();

        if (Input.GetMouseButton(1))
        {
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
