using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script qui charge les donn�es de GameStat
/// </summary>
public class SetupValue : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Dictionary<string, BoxCollider> _collidersDico; //Dico de toutes les zones du jeu
    [SerializeField] private GameObject[] _zoneObjects; //Tableau de tout les GameObject avec le script Encounters

    private void Awake()
    {
        _collidersDico = new Dictionary<string, BoxCollider>();

        foreach (GameObject zone in _zoneObjects) // Ajouter les BoxCollider des objets de zone dans le dictionnaire
        {
            string zoneName = zone.name;
            BoxCollider boxCollider = zone.GetComponent<BoxCollider>();
            _collidersDico.Add(zoneName, boxCollider);
        }

        foreach (KeyValuePair<string, bool> encounter in GameState.EncountersDico) // D�sactiver les zones qui ont �t� compl�t�es
        {
            string zoneName = encounter.Key;
            bool isCompleted = encounter.Value;

            if (isCompleted)
            {
                if (_collidersDico.ContainsKey(zoneName))
                {
                    BoxCollider boxCollider = _collidersDico[zoneName];
                    // D�sactiver le GameObject correspondant
                    boxCollider.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning($"Zone {zoneName} pr�sente dans GameStat mais pas dans _collidersDico.");
                }
            }
        }
        _player.transform.position = GameState.TeamPosition; //Setup la pos du joueur
    }
}
