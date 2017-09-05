using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public PlayerControl myPlayer;

    private Vector3 lastPlayerPosition;
    private float distanceToMove;

    // Use this for initialization
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerControl>();
        lastPlayerPosition = myPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the camera with the player.
        distanceToMove = myPlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastPlayerPosition = myPlayer.transform.position;
    }
}
