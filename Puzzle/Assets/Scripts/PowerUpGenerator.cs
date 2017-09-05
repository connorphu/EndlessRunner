using UnityEngine;
using System.Collections;

public class PowerUpGenerator : MonoBehaviour
{
    //Generating power ups.
    public ObjectPooler powerUpPool;
    public float distanceBetweenPowerUps;

    public void SpawnPowerUps(Vector3 startPosition)
    {
        GameObject powerUp = powerUpPool.GetPooledObject();
        powerUp.transform.position = startPosition;
        powerUp.SetActive(true);
    }
}
