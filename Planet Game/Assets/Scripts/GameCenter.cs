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

        Debug.Log("Level(" + (levelNumber+1) + ") Round(" + (roundNumber+1) + " of " + Levels[levelNumber].Rounds.Count + ") Wave(" + (waveNumber+1) + " of " + Levels[levelNumber].Rounds[roundNumber].Waves.Count + ") "+levelNumber+"/"+roundNumber+"-"+Levels[levelNumber].Rounds.Count+"/"+waveNumber+"-"+Levels[levelNumber].Rounds[roundNumber].Waves.Count);
        for (int i = 0; i < Levels[levelNumber].Rounds[roundNumber].Waves[waveNumber].sceneObjects.Count; i++)
        {
            #region Enemys
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
            #endregion
            #region PowerUps
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
            #endregion
        }
        waveNumber++;
    }
    private void Update()
    {
        
    }
    #endregion 
}
