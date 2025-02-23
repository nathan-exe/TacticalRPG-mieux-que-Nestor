using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace SimpleVFXs
{
    public class AutoDestroy : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] float Delay = 1;

        PooledObject _pooledObject;
        void OnInstantiatedByPool()
        {
            TryGetComponent<PooledObject>(out  _pooledObject);
        }

        void OnPulledFromPool()
        {
            _pooledObject.GoBackIntoPool_Delayed(Delay);
        }

        // Start is called before the first frame update
        void Start()
        {
            if(_pooledObject==null) Destroy(gameObject, Delay);
        }


    }
}
