using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //checkss if object was triggered by player
        if (col.gameObject.tag == "Player")
        {
            //triggers player death in the GameManager
            if(gameObject.tag == "DeathTrigger" || gameObject.tag == "Bat")
            {
                gm.playerIsDead = true;
                Destroy(col.gameObject);
                gm.CheckPlayerLives();
            }
            //triggers win screen
            else if(gameObject.tag == "WinTrigger")
            {
                gm.WinLevel();
            }
        }
    }
}
