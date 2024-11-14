using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameCenter : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Rigidbody playerRb;
    public int veiwedLevelNumber;
    public int veiwedRoundNumber;
    public int veiwedWaveNumber;

    public bool moonsAreInGame;

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

    public int levelNumber;
    public int roundNumber;
    public int waveNumber;

    public TextMeshProUGUI Level;

    private float countingDownTime;
    public float doubleCheck;
    public float checkFrequency;
    public float timeTillNewStage;
    //Level Data
    #region Level Pools
    public LevelData[] Levels;
    private void Start()
    {
        levelNumber = veiwedLevelNumber - 1;
        roundNumber = veiwedRoundNumber - 1;
        waveNumber = veiwedWaveNumber - 1;

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();

        //Debug.Log("Level(" + (levelNumber + 1) + ") Round(" + (roundNumber + 1) + " of " + Levels[levelNumber].Rounds.Count + ") Wave(" + (waveNumber + 1) + " of " + Levels[levelNumber].Rounds[roundNumber].Waves.Count + ") " + levelNumber + " / " + roundNumber + "-" + (Levels[levelNumber].Rounds.Count - 1) + " / " + waveNumber + "-" + (Levels[levelNumber].Rounds[roundNumber].Waves.Count - 1));
        StartCoroutine("SpawnWave");
    }
    private void Update()
    {
        countingDownTime -= Time.deltaTime;
        if (countingDownTime < 0)
        {
            countingDownTime += checkFrequency;
            //Debug.Log("Running");
            if (GameObject.FindGameObjectsWithTag("Moon").Length + GameObject.FindGameObjectsWithTag("Asteroid").Length == 0)
            {
                doubleCheck += checkFrequency;
            }
            else
            {
                doubleCheck = 0;
            }
            if (doubleCheck == timeTillNewStage)
            {
                StartCoroutine("SpawnWave");
                //Debug.Log("Spawning");
                doubleCheck = 0;
            }
            //Debug.Log(doubleCheck);
        }
    }
    #endregion 
    public IEnumerator SpawnWave()
    {
        if (Levels[levelNumber].Rounds[roundNumber].Waves.Count == waveNumber)
        {
            //Debug.Log("New Round");
            roundNumber++;
            waveNumber = 0;
        }
        if (waveNumber == 0)
        {
            playerRb.transform.position = new Vector3(0, 0.75f, 0);
        }
        Level.text = ("Level(" + (levelNumber + 1) + ") Round(" + (roundNumber + 1) + " of " + Levels[levelNumber].Rounds.Count + ") Wave(" + (waveNumber + 1) + " of " + Levels[levelNumber].Rounds[roundNumber].Waves.Count + ") ");
        Debug.Log("Spawning Wave!!! Level(" + (levelNumber + 1) + ") Round(" + (roundNumber + 1) + " of " + Levels[levelNumber].Rounds.Count + ") Wave(" + (waveNumber + 1) + " of " + Levels[levelNumber].Rounds[roundNumber].Waves.Count + ") " + (levelNumber+1) + " / " + (roundNumber+1) + "-" + (Levels[levelNumber].Rounds.Count) + " / " + (waveNumber+1) + "-" + (Levels[levelNumber].Rounds[roundNumber].Waves.Count));
        for (int i = 0; i < Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects.Count; i++)
        {
            if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject.Count == i)
            {
                Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject.Add(0);
            }
            yield return new WaitForSeconds(Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].timeBeforeObject[i]);
            #region Random Position Builder
            if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].x > 900 || Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].y > 900 || Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i].z > 900)
            {
                #region random position
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
                #endregion

                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E0")
                {
                    //Debug.Log("Enemy 0");
                    spawnManager.EnemyAsteroidGameObjectRetrieve(finalVector, (int) Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsValue[i]);
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E1")
                {
                    //Debug.Log("Enemy 1");
                    spawnManager.EnemyMoonGameObjectRetrieve(finalVector);
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E2")
                {
                   // Debug.Log("Enemy 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P0")
                {
                    //Debug.Log("PowerUp 0");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P1")
                {
                    //Debug.Log("PowerUp 1");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P2")
                {
                    //Debug.Log("PowerUp 2");
                }
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "P3")
                {
                    //Debug.Log("PowerUp 3");
                }
            }
            #endregion
            else
            {
                if (Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects[i] == "E0")
                {
                    Debug.Log("Enemy 0");
                    spawnManager.EnemyAsteroidGameObjectRetrieve(Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsPosition[i], (int)Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjectsValue[i]);
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
