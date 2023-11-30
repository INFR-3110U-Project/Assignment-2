using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //data to save
    public int currentScore;

    //how game starts if there's no loadable data
    public GameData()
    {
        currentScore = 0;
    }
}
