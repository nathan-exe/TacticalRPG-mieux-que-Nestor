using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    UniTask InteractWith();
    void OnSelected();
    void OnUnselected();

    
}
