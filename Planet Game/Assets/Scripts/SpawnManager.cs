using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    //Variables
    #region Variables
    //Impact Particles
    public GameObject impactParticleGameObject;
    public List<GameObject> impactParticlePool;
    private bool impactParticleNoAvailableObject;

    //Enemy on Enemy Collision
    public GameObject enemyOnEnemyParticleGameObject;
    public List<GameObject> enemyOnEnemyParticlePool;
    private bool enemyOnEnemyParticleNoAvailableObject;

    //Enemy Moon Pool
    public GameObject enemyMoonGameObject;
    public List<GameObject> enemyMoonGameObjectPool;
    private bool enemyMoonGameObjectNoAvailableObject;
    private Vector3 enemyMoonSpawnHight = new Vector3(0, 20, 0);
    #endregion

    //Functions
    #region ImpactParticle
    // Function to be called on requiring a Vector3
    public void ImpactParticleRetrieve(Vector3 impactPoint)
    {
        // Reset bool for later change
        impactParticleNoAvailableObject = true;
        // Check every pooled object
        for (int i = 0; i < impactParticlePool.Count; i++)
        {
            // Check if object is deactivated
            if (!impactParticlePool[i].activeInHierarchy)
            {
                // Reactivating Impact Particle
                Debug.Log("Reactivating Impact Particle Element (" + i + ")");
                // Controls the transportation, activeation & deactivation 
                StartCoroutine(ImpactParticleReturn(i, impactPoint));
                // Set bool false stopping next step from Instantiating new game object in pool
                impactParticleNoAvailableObject = false;
                // Tells for function to end
                break;
            }
        }
        // checks bool if Impact Particle was inabled, if not Instantiate new Impact Particle in pool
        if (impactParticleNoAvailableObject == true)
        {
            // Instantiating Impact Particle 
            Debug.Log("Instantiating Impact Particle Element (" + impactParticlePool.Count + ")");
            // Adds a clone of public game object to the pool
            impactParticlePool.Add(GameObject.Instantiate(impactParticleGameObject, impactPoint, Quaternion.identity));
            // Controls the transportation, activeation & deactivation
            StartCoroutine(ImpactParticleReturn(impactParticlePool.Count - 1, impactPoint));
        }
    }
    // Function with timed cooldown requires the effected Int GameObject & Vector3 transform position 
    IEnumerator ImpactParticleReturn(int impactPointUsedGameObject, Vector3 impactPoint)
    {
        // Transfers position to ImpactPoint vector & activates Impact Particle 
        impactParticlePool[impactPointUsedGameObject].transform.position = impactPoint;
        impactParticlePool[impactPointUsedGameObject].SetActive(true);
        // Time between top and bottom Function
        yield return new WaitForSeconds(1);
        // Transfers position to Spawn Manager position & deactivates Impact Particle
        impactParticlePool[impactPointUsedGameObject].SetActive(false);
        impactParticlePool[impactPointUsedGameObject].transform.position = transform.position;
    }
    #endregion
    #region Enemy on Enemy Particle
    // Function to be called on requiring a Vector3
    public void EnemyOnEnemyParticleRetrieve(Vector3 impactPoint)
    {
        // Reset bool for later change
        enemyOnEnemyParticleNoAvailableObject = true;
        // Check every pooled object
        for (int i = 0; i < enemyOnEnemyParticlePool.Count; i++)
        {
            // Check if object is deactivated
            if (!enemyOnEnemyParticlePool[i].activeInHierarchy)
            {
                // Reactivating EnemyOnEnemy Particle
                Debug.Log("Reactivating Enemy On Enemy Particle Element (" + i + ")");
                // Controls the transportation, activeation & deactivation 
                StartCoroutine(EnemyOnEnemyParticleReturn(i, impactPoint));
                // Set bool false stopping next step from Instantiating new game object in pool
                enemyOnEnemyParticleNoAvailableObject = false;
                // Tells for function to end
                break;
            }
        }
        // checks bool if enemyOnEnemy Particle was inabled, if not Instantiate new Impact Particle in pool
        if (enemyOnEnemyParticleNoAvailableObject == true)
        {
            // Instantiating enemyOnEnemy Particle 
            Debug.Log("Instantiating Enemy On Enemy Particle Element (" + enemyOnEnemyParticlePool.Count + ")");
            // Adds a clone of public game object to the pool
            enemyOnEnemyParticlePool.Add(GameObject.Instantiate(enemyOnEnemyParticleGameObject, impactPoint, Quaternion.identity));
            // Controls the transportation, activeation & deactivation
            StartCoroutine(EnemyOnEnemyParticleReturn(enemyOnEnemyParticlePool.Count - 1, impactPoint));
        }
    }
    // Function with timed cooldown requires the effected Int GameObject & Vector3 transform position 
    IEnumerator EnemyOnEnemyParticleReturn(int enemyOnEnemyUsedGameObject, Vector3 impactPoint)
    {
        // Transfers position to ImpactPoint vector & activates Impact Particle 
        enemyOnEnemyParticlePool[enemyOnEnemyUsedGameObject].transform.position = impactPoint;
        enemyOnEnemyParticlePool[enemyOnEnemyUsedGameObject].SetActive(true);
        // Time between top and bottom Function
        yield return new WaitForSeconds(1);
        // Transfers position to Spawn Manager position & deactivates Impact Particle
        enemyOnEnemyParticlePool[enemyOnEnemyUsedGameObject].SetActive(false);
        enemyOnEnemyParticlePool[enemyOnEnemyUsedGameObject].transform.position = transform.position;
    }
    #endregion
    #region Weak Enemys
    public void EnemyMoonGameObjectRetrieve(Vector3 spawnPoint)
    {
        // Reset bool for later change
        enemyMoonGameObjectNoAvailableObject = true;
        // Check every pooled object
        for (int i = 0; i < enemyMoonGameObjectPool.Count; i++)
        {
            // Check if object is deactivated
            if (!enemyMoonGameObjectPool[i].activeInHierarchy)
            {
                // Reactivating Enemy MoonGame Object
                Debug.Log("Reactivating Enemy MoonGame Object Element (" + i + ")");
                // Controls the transportation, activeation & deactivation 
                StartCoroutine(EnemyMoonGameObjectBirth(i, spawnPoint));
                // Set bool false stopping next step from Instantiating new game object in pool
                enemyMoonGameObjectNoAvailableObject = false;
                //Ends function
                break;
            }
        }
        // checks bool if Enemy MoonGame Object was inabled, if not Instantiate new Enemy MoonGame Object in pool
        if (enemyMoonGameObjectNoAvailableObject == true)
        {
            // Instantiating Impact Particle 
            Debug.Log("Instantiating Enemy MoonGame Object Element (" + enemyMoonGameObjectPool.Count + ")");
            // Adds a clone of public game object to the pool
            enemyMoonGameObjectPool.Add(GameObject.Instantiate(enemyMoonGameObject, spawnPoint, Quaternion.identity));
            // Controls the transportation, activeation & deactivation
            StartCoroutine(EnemyMoonGameObjectBirth(enemyMoonGameObjectPool.Count - 1, spawnPoint));
        }
    }
    public void RetrieveEnemy(int enemySpawnType, Vector3 spawnPoint)
    {
        switch (enemySpawnType)
        {
            case 0:
                //enemyMoonGameObjectPool.Add(GameObject.Instantiate(enemyMoonGameObject, spawnPoint, Quaternion.identity));
                break;
            case 1:
                #region Moon
                for (int i = 0; i < enemyMoonGameObjectPool.Count; i++)
                {
                    // Check if object is deactivated
                    if (!enemyMoonGameObjectPool[i].activeInHierarchy)
                    {
                        // Reactivating Enemy MoonGame Object
                        Debug.Log("Reactivating Enemy MoonGame Object Element (" + i + ")");
                        // Controls the transportation, activeation & deactivation 
                        StartCoroutine(EnemyMoonGameObjectBirth(i, spawnPoint));
                        // Set bool false stopping next step from Instantiating new game object in pool
                        enemyMoonGameObjectNoAvailableObject = false;
                        // Tells for function to end
                        break;
                    }
                }
                // checks bool if Enemy MoonGame Object was inabled, if not Instantiate new Enemy MoonGame Object in pool
                if (enemyMoonGameObjectNoAvailableObject == true)
                {
                    // Instantiating Impact Particle 
                    Debug.Log("Instantiating Enemy MoonGame Object Element (" + enemyMoonGameObjectPool.Count + ")");
                    // Adds a clone of public game object to the pool
                    enemyMoonGameObjectPool.Add(GameObject.Instantiate(enemyMoonGameObject, spawnPoint, Quaternion.identity));
                    // Controls the transportation, activeation & deactivation
                    StartCoroutine(EnemyMoonGameObjectBirth(enemyMoonGameObjectPool.Count - 1, spawnPoint));
                }
                // Reset bool for later change
                enemyMoonGameObjectNoAvailableObject = true;
                // Check every pooled object
                #endregion
                break;
            case 2:
                //enemyMoonGameObjectPool.Add(GameObject.Instantiate(enemyMoonGameObject, spawnPoint, Quaternion.identity));
                break;
        }
        
        
    }
    // Function with timed cooldown requires the effected Int GameObject & Vector3 transform position 
    IEnumerator EnemyMoonGameObjectBirth(int enemyMoonGameObjectUsedGameObject, Vector3 spawnPoint)
    {
        //Gives enemy a downward velocity
        enemyMoonGameObjectPool[enemyMoonGameObjectUsedGameObject].GetComponent<Rigidbody>().velocity = Vector3.down * 50;
        // Transfers position to Enemy MoonGame Object vector & activates Impact Particle 
        enemyMoonGameObjectPool[enemyMoonGameObjectUsedGameObject].transform.position = spawnPoint + enemyMoonSpawnHight;
        enemyMoonGameObjectPool[enemyMoonGameObjectUsedGameObject].SetActive(true);
        //moonScript = enemyMoonGameObjectPool[enemyMoonGameObjectUsedGameObject].GetComponent<MoonScript>();
        //Birth script needs to disable the disable movment bool
        // Time between top and bottom Function
        yield return new WaitForSeconds(0);
    }
    #endregion
    //look inti substrings
    
}