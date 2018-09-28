using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    //REQUIRED VARIABLES
    public int playerLives = 3;
    public GameObject playerPrefab;
    public bool playerIsDead;
    public Transform respawnLocation;
    public bool hasCheckpoint;
    public GameObject checkpoint;
    CameraFollow camScript;

    private GameObject player;

    //UI Objects
    public GameObject loseScreen;
    public GameObject winScreen;

    //Player Lives
    public Image playerLife;
    public Sprite threeHearts;
    public Sprite twoHearts;
    public Sprite oneHearts;
    public Sprite zeroHearts;

    //SOUNDS
    private AudioSource source;
    public AudioClip gameOverSound;
    public AudioClip gameWinSound;
    public GameObject backgroundMusic;

    // Use this for initialization
    void Start()
    {
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        source = GetComponent<AudioSource>();
        FindCurrentPlayerObject();
        playerLives = 3;
        playerLife.sprite = threeHearts;
    }

    //Checks to see how many lives the player has left
    public void CheckPlayerLives()
    {
        if (playerIsDead)
        {
            playerLives--;
            player = null;
            if (playerLives > 0)
            {
                camScript.enabled = !camScript.enabled;
                //Respawns
                RespawnPlayer();
            }
            else
            {
                //Ends Game
                TriggerGameover();
            }
            //Changes the sprite for player life
            UpdateLives();
        }
    }

    //Resets the players location to their last spawnpoint upon death
    void RespawnPlayer()
    {
        if (hasCheckpoint)
        {
            Instantiate(playerPrefab, respawnLocation.transform.position, respawnLocation.transform.rotation);
        }
        else if (!hasCheckpoint)
        {
            Instantiate(playerPrefab, respawnLocation.transform.position, respawnLocation.transform.rotation);
        }
        camScript.enabled = enabled;
        //Resets the camera to the player
        camScript.ResetCamera();
        playerIsDead = false;
        //Find new player object
        FindCurrentPlayerObject();
    }

    //End the game
    public void TriggerGameover()
    {
        //Show Lose Screen
        loseScreen.SetActive(true);
        //Turn off background music
        backgroundMusic.SetActive(false);
        //Play Game Over sound
        source.PlayOneShot(gameOverSound);
    }

    //Changes to player life sprite to represent how many lives the player has left at any given moment.
    void UpdateLives()
    {
        if (playerLives == 3)
        {
            playerLife.sprite = threeHearts;
        }
        else if (playerLives == 2)
        {
            playerLife.sprite = twoHearts;
        }
        else if (playerLives == 1)
        {
            playerLife.sprite = oneHearts;
        }
        else if (playerLives == 0)
        {
            playerLife.sprite = zeroHearts;
        }
    }

    //Ends the level, and sets the player up to move on to the next one
    public void WinLevel()
    {
        //Show Win Screen
        winScreen.SetActive(true);
        //Turn off background music
        backgroundMusic.SetActive(false);
        //Play losing sound
        source.PlayOneShot(gameWinSound);
        //Turn off character controller
        FindCurrentPlayerObject();
        player.SetActive(false);
    }

    //Consistently runs checks to determine where the player is
    void FindCurrentPlayerObject()
    {
        //Find player object marked as "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
