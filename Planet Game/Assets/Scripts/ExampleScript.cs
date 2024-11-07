using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExampleScript : MonoBehaviour
{
    #region Variables
    public GameObject exampleGameObject; // The GameObject created each time there is no available .
    public List<GameObject> exampleGameObjectPool; // The pool holding every (exampleGameObject).
    private bool exampleGameObjectNoAvailableObject; // A true or false bool used for logic in (exampleGameObject) creation.
    #endregion
    
    #region Example Function Retrieve 
    public void ExampleFunctionName(Vector3 exampleVector3) // The function to Retrieve a (exampleGameObject) from the inactive (exampleGameObject) in the (exampleGameObjectPool).
    {
        exampleGameObjectNoAvailableObject = true; // Sets bool to true every time function is run to insure a unsuccessful for() loop will proceed to (exampleGameObject) creation // line 27.
        for (int i = 0; i < exampleGameObjectPool.Count; i++) // Runs once for each (exampleGameObject) in (exampleGameObjectPool).
        {
            if (!exampleGameObjectPool[i].activeInHierarchy) // Checks each (exampleGameObject) if they are NOT active.
            {
                exampleGameObjectNoAvailableObject = false; // Disables the if() conditional preventing the creation of a new (exampleGameObject) // line 27.
                exampleGameObjectPool[i].transform.position = exampleVector3; // Moves (exampleGameObject) to the given Vector3 position.
                exampleGameObjectPool[i].SetActive(true); // Sets (exampleGameObject) active state to true.
                break; // Terminates for() Loop preventing the for() from finding other available objects.
            }
        }
        if (exampleGameObjectNoAvailableObject == true) // We can run this script if there are no available (exampleGameObject)'s otherwise the for() Loop will disable this if() statment.
        {
            exampleGameObjectPool.Add(GameObject.Instantiate(exampleGameObject, exampleVector3, Quaternion.identity)); // Creates a new (exampleGameObject) and adds it into the pool.
            exampleGameObjectPool[exampleGameObjectPool.Count - 1].transform.position = exampleVector3; // Moves (exampleGameObject) to the given Vector3 position.
            exampleGameObjectPool[exampleGameObjectPool.Count - 1].SetActive(true); // Sets (exampleGameObject) active state to true.
        }
    }
    #endregion

    #region Example Function Return
    public void ImpactParticleReturn(int exampleGameObjectPoolNumber) // The function to return the (exampleGameObject) back to the inactive pool.
    {
        exampleGameObjectPool[exampleGameObjectPoolNumber].SetActive(false); // Sets (exampleGameObject) active state to false.
        exampleGameObjectPool[exampleGameObjectPoolNumber].transform.position = transform.position; // Returns (exampleGameObject) to SpawnManager position.
    }
    #endregion
}