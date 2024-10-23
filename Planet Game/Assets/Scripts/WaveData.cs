using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
    public List<GameObject> sceneObjects;
    public List<Vector3> sceneObjectsPosition;
    public List<Vector3> sceneObjectsRotation;
    public List<float> sceneObjectsValue;
}
