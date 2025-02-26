using System;
using System.Reflection;
using UnityEngine;
/// <summary>
/// Struct de player pour save plus facilement
/// </summary>
public class CharacterState
{
    public float HP;
    public float Mana;

    public string DataFileName;

    public EntityData EntityData;

    public CharacterState() { }

    public CharacterState(string DataFileName ,float HP = -1 )
    {
        this.DataFileName = DataFileName;
        this.EntityData = (EntityData)Resources.Load(DataFileName);
        if (HP == -1) this.HP = EntityData.MaxHP; else this.HP = HP;
        Mana = CombatEntity.MaxManaPerEntity;        
    }


}
