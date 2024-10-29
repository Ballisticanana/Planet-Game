using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    private SpawnManager spawnManager;
    public int veiwedLevelNumber;
    public int veiwedRoundNumber;
    public int veiwedWaveNumber;
    #region Random Position Variables
    // Format 012.3456
    // (0)(1)(2).(34)(56)
    // (0) = classifier to make randome = 9
    // (1) = minimum value positve or negitive 1 = -1 || 2 = 1
    // (2) = maximum value positve or negitive 1 = -1 || 2 = 1
    // (34) = minimum value = Two digit whole number
    // (56) = maximum value = Two digit whole number
    // Random.RandomRange((1) * (34),(2) * (56))
    // X Random
    private float posXCripticFloat;
    private float posXNumberOnePosOrNeg;
    private float posXNumberTwoPosOrNeg;
    private float posXNumberOneRangeMin;
    private float posXNumberTwoRangeMax;
    private float posX;
    // Y Random
    private float posYCripticFloat;
    private float posYNumberOnePosOrNeg;
    private float posYNumberTwoPosOrNeg;
    private float posYNumberOneRangeMin;
    private float posYNumberTwoRangeMax;
    private float posY;
    // Z Random
    private float posZCripticFloat;
    private float posZNumberOnePosOrNeg;
    private float posZNumberTwoPosOrNeg;
    private float posZNumberOneRangeMin;
    private float posZNumberTwoRangeMax;
    private float posZ;

    private Vector3 finalVector;
    #endregion

    private int levelNumber;
    private int roundNumber;
    private int waveNumber;
    //Level Data
    #region Level Pools
    public LevelData[] Levels;
    private void Start()
    {
        levelNumber = veiwedLevelNumber - 1;
        roundNumber = veiwedRoundNumber - 1;
        waveNumber = veiwedWaveNumber - 1;

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        Debug.Log("Level(" + (levelNumber + 1) + ") Round(" + (roundNumber + 1) + " of " + Levels[levelNumber].Rounds.Count + ") Wave(" + (waveNumber + 1) + " of " + Levels[levelNumber].Rounds[roundNumber].Waves.Count + ") " + levelNumber + "/" + roundNumber + "-" + (Levels[levelNumber].Rounds.Count - 1) + "/" + waveNumber + "-" + (Levels[levelNumber].Rounds[roundNumber].Waves.Count - 1));
        StartCoroutine("SpawnInterval");
    }
    #endregion 
    IEnumerator SpawnInterval()
    {
        for (int i = 0; i < Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects.Count; i++)
        {
            if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject.Count == i)
            {
                Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject.Add(0);
            }
            #region Random Position Builder
            if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x > 900 || Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].y > 900 || Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].z > 900)
            {
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x > 900)
                {
                    RandomPositionGeneratorX(i);
                    finalVector.x = posX;
                }
                else
                {
                    finalVector.x = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x;
                }

                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].y > 900)
                {
                    RandomPositionGeneratorY(i);
                    finalVector.y = posY;
                }
                else
                {
                    finalVector.y = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].y;
                }

                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].z > 900)
                {
                    RandomPositionGeneratorZ(i);
                    finalVector.z = posZ;
                }
                else
                {
                    finalVector.z = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].z;
                }

                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E0")
                {
                    Debug.Log("Enemy 0");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E1")
                {
                    Debug.Log("Enemy 1");
                    spawnManager.EnemyMoonGameObjectRetrieve(finalVector);
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E2")
                {
                    Debug.Log("Enemy 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P0")
                {
                    Debug.Log("PowerUp 0");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P1")
                {
                    Debug.Log("PowerUp 1");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P2")
                {
                    Debug.Log("PowerUp 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P3")
                {
                    Debug.Log("PowerUp 3");
                }
            }
            #endregion
            else
            {
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E0")
                {
                    Debug.Log("Enemy 0");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E1")
                {
                    Debug.Log("Enemy 1");
                    spawnManager.EnemyMoonGameObjectRetrieve(Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i]);
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E2")
                {
                    Debug.Log("Enemy 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P0")
                {
                    Debug.Log("PowerUp 0");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P1")
                {
                    Debug.Log("PowerUp 1");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P2")
                {
                    Debug.Log("PowerUp 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P3")
                {
                    Debug.Log("PowerUp 3");
                }
            }
            yield return new WaitForSeconds(Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject[i]);
        }
        waveNumber++;
    }
    #region Random Position X, Y, Z Functions
    public void RandomPositionGeneratorX(int i)
    {
        posXCripticFloat = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x % 900;
        if (Mathf.Round(posXCripticFloat / 10) == 2)
        {
            posXNumberOnePosOrNeg = 1;
        }
        else
        {
            posXNumberOnePosOrNeg = -1;
        }
        if (Mathf.Round((posXCripticFloat%10)) == 2)
        {
            posXNumberTwoPosOrNeg = 1;
        }
        else
        {
            posXNumberTwoPosOrNeg = -1;
        }
        posXNumberOneRangeMin = Mathf.Round((posXCripticFloat % 1) * 100);
        posXNumberTwoRangeMax = Mathf.Round((posXCripticFloat % 0.01f) * 10000);
        posX = Random.Range(posXNumberOnePosOrNeg * posXNumberOneRangeMin, posXNumberTwoPosOrNeg * posXNumberTwoRangeMax);
    }
    public void RandomPositionGeneratorY(int i)
    {
        posYCripticFloat = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].y % 900;
        if (Mathf.Round(posYCripticFloat / 10) == 2)
        {
            posYNumberOnePosOrNeg = 1;
        }
        else
        {
            posYNumberOnePosOrNeg = -1;
        }
        if (Mathf.Round((posYCripticFloat%10)) == 2)
        {
            posYNumberTwoPosOrNeg = 1;
        }
        else
        {
            posYNumberTwoPosOrNeg = -1;
        }
        posYNumberOneRangeMin = Mathf.Round((posYCripticFloat % 1) * 100);
        posYNumberTwoRangeMax = Mathf.Round((posYCripticFloat % 0.01f) * 10000);
        posY = Random.Range(posYNumberOnePosOrNeg * posYNumberOneRangeMin, posYNumberTwoPosOrNeg * posYNumberTwoRangeMax);
    }
    public void RandomPositionGeneratorZ(int i)
    {
        posZCripticFloat = Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].z % 900;
        if (Mathf.Round(posZCripticFloat / 10) == 2)
        {
            posZNumberOnePosOrNeg = 1;
        }
        else
        {
            posZNumberOnePosOrNeg = -1;
        }
        if (Mathf.Round((posZCripticFloat%10)) == 2)
        {
            posZNumberTwoPosOrNeg = 1;
        }
        else
        {
            posZNumberTwoPosOrNeg = -1;
        }
        posZNumberOneRangeMin = Mathf.Round((posZCripticFloat % 1) * 100);
        posZNumberTwoRangeMax = Mathf.Round((posZCripticFloat % 0.01f) * 10000);
        posZ = Random.Range(posZNumberOnePosOrNeg * posZNumberOneRangeMin, posZNumberTwoPosOrNeg * posZNumberTwoRangeMax);
    }
    #endregion
}
