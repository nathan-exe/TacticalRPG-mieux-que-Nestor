using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Ce script n'est pas un singleton car on peut avoir une pool par prefab dans la scène,avec chacune des tailles différentes.
/// Dans un plus gros projet, on pourrait coder un script poolmanager qui serait un singleton avec dedans toutes les pools et des enums pour faire spawn n'importe quelle prefab facilement etc, mais KISS.
/// 
/// Messages optionnels envoyés aux objets de la pool:
/// - OnInstantiatedByPool
/// - OnPulledFromPool
/// - OnPutBackIntoPool
/// </summary>
public class Pool : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] uint _poolSize = 50;

    /*[SerializeField] */
    private PooledObject[] _instances; //toutes les instances d'objets, actives comme inactives.
    /*[SerializeField] */
    private List<int> _freeIndices = new List<int>(); //contient tous les indices des instances inactives dans le tableau _instances

    private void Awake()
    {
        PopulatePool();
    }

    /// <summary>
    /// peuple la pool en instanciant des GameObjects ayant tous un component PooledObject
    /// </summary>
    private void PopulatePool()
    {
        //création du tableau d'instances
        _instances = new PooledObject[_poolSize];

        //regarde si il y'aura besoin d'ajouter le component PooledObject aux instances de la prefab,ou si il y est déjà.
        bool PrefabAlreadyHasPooledObjectComponent = _prefab.GetComponent<PooledObject>();

        //crée toutes les instances de la prefab et les met dans la pool.
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject instancedObject = GameObject.Instantiate(_prefab);
            instancedObject.name += i.ToString();

            PooledObject po = PrefabAlreadyHasPooledObjectComponent ? instancedObject.GetComponent<PooledObject>() : instancedObject.AddComponent<PooledObject>();
            po.Index = i;
            po.Pool = this;
            po.IsInPool = true;
            _instances[i] = po;

            //message optionnel
            instancedObject.SendMessage("OnInstantiatedByPool", SendMessageOptions.DontRequireReceiver);

            PutObjectBackInPool(po);

        }
    }


    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool. à appeler à la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    /// <param name="Parent"></param>
    /// <returns></returns>
    public GameObject PullObjectFromPool(Transform Parent = null)
    {
        Assert.IsTrue(_freeIndices.Count > 0, "Pool is empty!");

        //pioche le premier indice libre dans la pool.
        int id = _freeIndices[0];
        _freeIndices.Remove(id);

        //activation de l'objet sélectionné
        PooledObject o = _instances[id];
        o.IsInPool = false;
        o.gameObject.SetActive(true);
        o.transform.parent = Parent;

        //message optionnel
        o.gameObject.SendMessage("OnPulledFromPool", SendMessageOptions.DontRequireReceiver);
        return o.gameObject;
    }

    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool à une certaine position. 
    /// à appeler à la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    public GameObject PullObjectFromPool(Vector3 Position, Transform Parent = null)
    {
        GameObject o = PullObjectFromPool(Parent);
        o.transform.position = Position;
        return o;
    }

    /// <summary>
    /// permet de faire "spawn" un gameObject inactif depuis la pool à une certaine position et rotation. 
    /// à appeler à la place de GAMEOBJECT.INSTANTIATE()
    /// </summary>
    public GameObject PullObjectFromPool(Vector3 Position, Quaternion rotation, Transform Parent = null)
    {
        GameObject o = PullObjectFromPool(Position, Parent);
        o.transform.rotation = rotation;
        return o;
    }



    /// <summary>
    /// desactive un objet et le remet dans la pool.
    /// le component PooledObject contient également une méthode GoBackIntoPool() pour faire "despawn" l'objet facilement.
    /// </summary>
    /// <param name="ObjectToPool"></param>
    public void PutObjectBackInPool(PooledObject ObjectToPool)
    {
        if (!_freeIndices.Contains(ObjectToPool.Index))
        {
            _freeIndices.Add(ObjectToPool.Index);
            ObjectToPool.gameObject.SendMessage("OnPutBackIntoPool", SendMessageOptions.DontRequireReceiver);

        }

        ObjectToPool.transform.parent = transform;
        ObjectToPool.gameObject.SetActive(false);
        ObjectToPool.IsInPool = true;



    }


}
