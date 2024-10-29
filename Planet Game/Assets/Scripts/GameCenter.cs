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
    public float posXCripticFloat;
    public float posXNumberOnePosOrNeg;
    public float posXNumberTwoPosOrNeg;
    public float posXNumberOneRangeMin;
    public float posXNumberTwoRangeMax;


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
            if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x > 900)
            {
                RandomPositionGeneratorX(i);
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
        //
        Random.RandomRange(posXNumberOnePosOrNeg * posXNumberOneRangeMin, posXNumberTwoPosOrNeg * posXNumberOneRangeMax);
        //
        //posXNumberTwoPosOrNeg
        //posXNumberOneRangeMin
        //posXNumberTwoRangeMax
        //float min     float max
        Random.RandomRange(posXNumberOnePosOrNeg * posXNumberOneRangeMin, posXNumberTwoPosOrNeg * posXNumberOneRangeMax);


    }
}
