using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Setup/Game Data")]
public class GameData : ScriptableObject
{
    public GameData() {}

    public int maxStagesReached;

    public TurretsData turretsData;

    
}
