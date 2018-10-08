using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=O6wlIqe2lTA used code from this video and adapted the WasWithPlayer method 
public class SideToSide : MonoBehaviour
{
    public bool moving = false;

    //Is entered in editor
    [SerializeField]
    private Vector3 velocity; 

    //Sets the moveing bool to true so that the fixed update if statement can be called which moves the platform
    //Additionally the player is set to be a child of the platform
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(WasWithPlayer(other))
        {
            moving = true;
            other.collider.transform.SetParent(transform);
        }
    }

    //When the character exits the collider it sets its parent to be null
    private void OnCollisionExit2D(Collision2D other)
    {
        if (WasWithPlayer(other))
        {
            other.collider.transform.SetParent(null);
        }
    }

    //Moves the platform when the bool variable is true
    private void FixedUpdate()
    {
        if(moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }

    //Returns a boolean true or false if the thing that is colliding with the platform is the player
    public static bool WasWithPlayer(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
