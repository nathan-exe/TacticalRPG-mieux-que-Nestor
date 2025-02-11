using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FightEnd : MonoBehaviour
{
    public void EndGame(GameObject EntityDeath)
    {
        if(PlayerCombatEntity.Instances.Count == 0)
        {
            print("GameOver");
        }
        if (AiCombatEntity.Instances.Count == 0)
        {
            print("Victoire");
        }
    }
}
