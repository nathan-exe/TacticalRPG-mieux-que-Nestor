using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FileManager 
{
    protected FileManager()
    {

    }

    public abstract void SaveData(GameData data);
    public abstract GameData LoadData();


}
