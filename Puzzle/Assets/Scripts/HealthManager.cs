using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public int healthCount;
    private Animator healthAnimator;

    public PlayerControl myPlayer;

	// Use this for initialization
	void Start ()
    {
        //Set Health to full.
        healthCount = myPlayer.health;
        healthAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Allow game to know what state the health is at.
        healthCount = myPlayer.health;
        healthAnimator.SetInteger("healthState", healthCount);
	}
}
