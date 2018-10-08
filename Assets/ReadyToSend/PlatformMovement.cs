using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    //Taken from https://www.youtube.com/watch?v=77Z-IghlEFw
    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nexPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    // Use this for initialization
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }
    private void Move()
    {   
        //Called in update, attemps to move the object to the nexpos conistantly, by enabling a child transform with a speed multiplied by seconds 
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);

        //Changes the nextposition variable by checking if the distance between the platform and the next position is less than or equal to < 0.1, meaning it is on top of it 
        if (Vector3.Distance(childTransform.localPosition, nexPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        //The next position is going to be posA if it does not equal posA, if it does go to position b
        nexPos = nexPos != posA ? posA : posB;
    }

    //Handles the player as a child of the platform that they land on in to allow proper movement with the platform
    private void OnCollisionEnter2D(Collision2D other)
    {
        //On collision if the game object entering the collision is player set them to be a child
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 8;
            other.transform.SetParent(childTransform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        //On exiting the collision set the parent to null in order to resume normal movement
        other.transform.SetParent(null);
    }

}