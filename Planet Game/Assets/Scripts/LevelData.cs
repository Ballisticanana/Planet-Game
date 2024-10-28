using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Levels", order = 1)]
public class LevelData : ScriptableObject
{
    public List<RoundData> Rounds;
}
