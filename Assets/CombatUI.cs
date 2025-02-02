using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// big singleton avec plein de références dedans
/// </summary>
public class CombatUI : MonoBehaviour
{

    [Header("References")]
    [field: SerializeField] public SpellSelectionPanel SpellSelectionPanel;

    //Singleton
    public static CombatUI Instance;
    private void Awake()
    {
        if(Instance!=null && Instance!=this) Destroy(this);
        else Instance = this;
    }
}
