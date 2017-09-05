using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    //Managing score.
    public Text scoreText;
    public Text highScoreText;

    public float scoreCount;
    public float highScoreCount;
    public float pointsPerSec;

    public PowerUps powerUp;
    public PlayerControl myPlayer;

	// Use this for initialization
	void Start ()
    {
        //Allows players to save previous high scores.
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Score stops when player dies.
        if(myPlayer.liveState)
        {
            scoreCount += pointsPerSec * Time.deltaTime;

            if (myPlayer.doublePointsState)
            {
                scoreCount += (2 * pointsPerSec * Time.deltaTime);

                powerUp.doublePointsDuration -= Time.deltaTime;
                if(powerUp.doublePointsDuration <= 0)
                {
                    scoreCount += pointsPerSec * Time.deltaTime;
                    myPlayer.doublePointsState = false;
                    myPlayer.powerUpState = false;
                    powerUp.doublePointsDuration = powerUp.startPowerUpDuration;
                }
                
            }
        }

        //Setting new high scores.
        if(scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
	}
}
