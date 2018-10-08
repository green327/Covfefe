using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    Vector3 direction = Vector3.left;

    // Use this for initialization
    void Start() {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //moves to one direction
        transform.Translate(direction * Time.deltaTime * speed);
        //check to see if bat reached the patrol point
        if (Vector3.Distance(this.transform.position, currentPatrolPoint.position) < .1f)
        {
            //Debug.Log("test");
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        //Debug.Log(currentPatrolIndex % 2);
        if (currentPatrolIndex % 4 == 0 && currentPatrolIndex  == 0)
        {
            direction = Vector3.right;
            //Debug.Log("right");
        }
        else if(currentPatrolIndex % 4 == 1)
        {
            direction = Vector3.up;
            //Debug.Log("up");
        }
        else if(currentPatrolIndex % 4 == 2)
        {
            direction = Vector3.left;
            //Debug.Log("left");
        }
        else if (currentPatrolIndex % 4 == 3)
        {
            direction = Vector3.down;
            //Debug.Log("down");
        }
        currentPatrolIndex++;

        //checks to see if there are any more patrol points in the array
        if (currentPatrolIndex + 1 > patrolPoints.Length)
            currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        //Debug.Log(currentPatrolIndex);

    }
}

