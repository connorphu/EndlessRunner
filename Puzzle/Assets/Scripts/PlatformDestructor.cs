using UnityEngine;
using System.Collections;

public class PlatformDestructor : MonoBehaviour
{
    public GameObject destructionPoint;
	// Use this for initialization
	void Start ()
    {
        destructionPoint = GameObject.Find("Destruction Point");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Unactivate objects if their x position is less than destruction point.
	    if(transform.position.x < destructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
	}
}
