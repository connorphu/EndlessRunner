using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    public PlayerControl myPlayer;
    public float tornadoDuration;
    public float doublePointsDuration;
    public float startPowerUpDuration;
    private int powerUpSelector;

    public AudioSource powerUpSound;

    // Use this for initialization
    void Start()
    {
        powerUpSelector = Random.Range(0, 100);
    }

    //Allow players to have power ups.
    //45% Tornado, 45% double points, 10% half health
    //Then disable power up coin.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !myPlayer.powerUpState)
        {
            myPlayer.powerUpState = true;
            powerUpSound.Play();
            if (powerUpSelector >= 0 && powerUpSelector <= 44)
            {
                myPlayer.tornadoState = true;
            }
            else if (powerUpSelector >= 45 && powerUpSelector <= 89)
            {
                myPlayer.doublePointsState = true;
            }
            else if (powerUpSelector >= 90 && powerUpSelector <= 99)
            {
                myPlayer.health = 1;
                myPlayer.powerUpState = false;
            }
            powerUpSelector = Random.Range(0, 100);
        }
        gameObject.SetActive(false);
    }
}
