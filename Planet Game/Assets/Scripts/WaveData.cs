using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]//(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)
public class WaveData : ScriptableObject
{
    public List<string> sceneObjects;
    public List<Vector3> sceneObjectsPosition;
    public List<Vector3> sceneObjectsRotation;
    public List<float> sceneObjectsValue;
}