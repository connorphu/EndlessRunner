using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    //Make a pool of objects to be used in game.
    public GameObject poolObject;
    public int pooledAmount;

    List<GameObject> pooledObjects;

	// Use this for initialization
	void Start ()
    {
        //Populating objects pool.
        pooledObjects = new List<GameObject>();     
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject) Instantiate(poolObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}
	
    //Access to the objects of the list.
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject obj = (GameObject) Instantiate(poolObject);
        obj.SetActive(false);
        return obj;
    }
}
