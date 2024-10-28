using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]//(fileName = "Round", menuName = "ScriptableObjects/Rounds", order = 1)
public class RoundData : ScriptableObject
{
    public List<WaveData> Waves;
}
