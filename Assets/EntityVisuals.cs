using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVisuals : MonoBehaviour
{
    [SerializeField] Transform _mesh;

    private void Awake()
    {
        if (_mesh == null) _mesh = transform.Find("Visuals");//GetComponentInChildren<MeshRenderer>().transform;
    }

    public async UniTask shake()
    {
        //pas terrible
        float duration = .4f;
        _mesh.DOShakePosition(duration, 1.2f,20);
        await UniTask.Delay(Mathf.RoundToInt(duration * 1000)); 
    }

    public async UniTask Jump()
    {
        //pas terrible
        float duration = .25f;
        _mesh.DOJump(_mesh.position,1,1, duration);
        await UniTask.Delay(Mathf.RoundToInt(duration * 1000));
    }
}
