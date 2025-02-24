using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaDrop : MonoBehaviour
{
    Light _light;

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent(out _light);
    }

    private async void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger !");
        if(other.gameObject.TryGetComponent(out CombatEntity entity))
        {
            Debug.Log("trigger ! AHHHHHHH");
            entity.Mana += 5;
            transform.DOScale(0, .5f);
            _light.DOIntensity(0, .5f);
            await UniTask.Delay(500);
            transform.position= Graph.Instance.FreeNodes.PickRandom().pose.X0Y()+Vector3.up*0.5f;
            transform.DOScale(1, .5f);
            _light.DOIntensity(8, .5f);

        }
    }

    private IEnumerator Start()
    {
        transform.localScale = Vector3.zero;
        _light.intensity = 0;
        yield return 0;
        transform.position = Graph.Instance.FreeNodes.PickRandom().pose.X0Y() + Vector3.up * 0.5f;
        transform.DOScale(1, .5f);
        _light.DOIntensity(8, .5f);
    }
}
