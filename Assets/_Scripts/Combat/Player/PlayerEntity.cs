using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerEntity : Entity
{
    public async void PlayerPlay()
    {
        await Spells.Execute();
    }
    
    
    /*player.play():
    - await ChooseDestination()
    - await MoveToDestination()
    - await ChooseSpell()
    - await Spell.Execute()*/
}
