using UnityEngine;
using System.Collections;

public class GroundGenerator : MonoBehaviour
{
    public Transform generationPoint;
    public float distanceBetween;
    public float distanceMin;
    public float distanceMax;
    public ObjectPooler[] objectPools;

    public Transform maxHeightPoint;
    private float minHeight;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private int platformSelector;
    private float[] platformWidths;

    private PowerUpGenerator powerUpGenerator;
    private int powerUpChance;

	// Use this for initialization
	void Start ()
    {
        //Set platform widths.
        platformWidths = new float[objectPools.Length];
        for(int i = 0; i < objectPools.Length; i++)
        {
            platformWidths[i] = objectPools[i].poolObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        powerUpGenerator = FindObjectOfType<PowerUpGenerator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Generates a platform whenever a platform's x position is less than generation point.
	    if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceMin, distanceMax);
            platformSelector = Random.Range(0, objectPools.Length);
            powerUpChance = Random.Range(0, 99);

            //Changes height of platforms.
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);
            
            //Generating new platforms.
            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //Generates power ups on top on platforms with 25% chance.
            if(powerUpChance >= 0 && powerUpChance <= 24)
            {
                powerUpGenerator.SpawnPowerUps(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
	}
}
