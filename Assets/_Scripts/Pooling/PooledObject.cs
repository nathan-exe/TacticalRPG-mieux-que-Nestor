using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ce component permet à un objet de se souvenir de la pool de laquelle il vient, ainsi que de son index dedans.
/// il a également une méthode pour désactiver l'objet et le renvoyer dans la pool.
/// </summary>
public class PooledObject : MonoBehaviour
{
    //ces variables sont gérées automatiquement par la pool à laquelle appartient l'objet.
    public bool IsInPool = true;
    public int Index;
    public Pool Pool;

    List<Behaviour> _enabledBehavioursAtStart = new();

    //réactive tous les components activés de base et inversement
    void OnInstantiatedByPool()
    {
        foreach(Behaviour comp in GetComponents<Behaviour>()) if(comp.enabled) _enabledBehavioursAtStart.Add(comp);
    }
    void OnPulledFromPool()
    {
        foreach (Behaviour comp in GetComponents<Behaviour>()) comp.enabled = _enabledBehavioursAtStart.Contains(comp);
    }

    /// <summary>
    /// à utiliser à la place de Destroy(gameObject)
    /// </summary>
    public void GoBackIntoPool()
    {
        if(!IsInPool) Pool.PutObjectBackInPool(this);
    }
    public void GoBackIntoPool_Delayed(float delay)
    {
        StartCoroutine(C_GoBackIntoPool_Delayed(delay));
    }

    IEnumerator C_GoBackIntoPool_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Application.isPlaying)
        if (!IsInPool) Pool.PutObjectBackInPool(this);
    }

}
