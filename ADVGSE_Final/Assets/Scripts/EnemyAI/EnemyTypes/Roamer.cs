using System.Runtime.CompilerServices;
using UnityEngine;

public enum RoamingDirection
{
    TowardsPlayer, // (Towards -z)
    AwayFromPlayer, //enemies moving "AwayFromPlayer" should not be faster than player (Towards +z)
    ToRight, //(Towards +x)
    ToLeft, //(Towards -x)

}


public class Roamer : MonoBehaviour
{
    /// <summary>
    /// Direction that this enemy is facing and moving towards
    /// </summary>
    [SerializeField] RoamingDirection movementDirection;

    /// <summary>
    /// Enemy's movement speed.
    /// </summary>
    [SerializeField] float movementSpeed;

    /// <summary>
    /// Steps enemy takes before they do a 360 and walk back
    /// </summary>
    [SerializeField] float stepsBeforeTurn;

    private float timeWalked;

    /// <summary>
    /// time it takes for enemy to complete 180 degree turn
    /// </summary>
    private float timeOfTurn;

    /// <summary>
    /// Rotation needed for enemy to be facing correct way.
    /// </summary>
    private int enemyRot = 0;




    private void Awake()
    {
        timeWalked = stepsBeforeTurn;

        //rotates enemy based off which direction it is supposed to be facing
        switch(movementDirection)
        {
            case RoamingDirection.TowardsPlayer:
                enemyRot = -180;
                break;

            case RoamingDirection.AwayFromPlayer:
                enemyRot = 0;
                break;

            case RoamingDirection.ToLeft:
                enemyRot = -90;
                break;

            case RoamingDirection.ToRight:
                enemyRot = -360;
                break;

        }

        //changes facing direction when enemy is created based on enum type
        transform.rotation = Quaternion.Euler(0, enemyRot, 0);
    }



    private void Update()
    {
        timeWalked -= Time.deltaTime;

        //Rotates enemy 180 when they've walked designated amount.
        if (timeWalked < 0 )
        {
            //calculates quat rot with 180 rotation
            Quaternion tempRot = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);

            //slerp rotation between current and turned 180 rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, timeOfTurn);

            //Resets timer until next 180
            timeWalked = stepsBeforeTurn;

            Debug.Log("This code is actually happening.");
        }

        transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
    }
}
