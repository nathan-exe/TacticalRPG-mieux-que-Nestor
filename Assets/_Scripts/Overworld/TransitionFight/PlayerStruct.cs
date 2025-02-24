using System.Reflection;
using UnityEngine;
/// <summary>
/// Struct de player pour save plus facilement
/// </summary>
public struct CharacterState
{
    public float HP;
    public float Mana;
    public string Name;

    public EntityData EntityData;

    public CharacterState(string DataFileName)
    {
        this.EntityData = (EntityData)Resources.Load(DataFileName);
        
        HP = EntityData.MaxHP;
        Mana = CombatEntity.MaxManaPerEntity;
        Name = EntityData.Name;
        
    }
}
