using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
    //Collectible Variables
	public int collectiblesTotal;
	public int collectiblesCollected = 0;
	public Text collectiblesTotalText;
	public Text collectibleCollectedText;

    //Timer Bar Variables
    public float maxTime;
    private float timeRemaining;
    public Image timerBar;

    //GameManager Variable
    GameObject gm;

    //Everything that is needed at the start of the level
    void Start () 
	{
        gm = GameObject.FindGameObjectWithTag("GameManager");
        timeRemaining = maxTime;
        CountTotalCollectiblesAvailable();
    }

    //Everything that needs to be called each frame
    void Update()
    {
        TimerDecrease();
    }

    //Counts all collectables placed within a scene and updates the UI to represent that number
    void CountTotalCollectiblesAvailable()
	{
		collectiblesTotal = GameObject.FindGameObjectsWithTag("Collectible").Length;
		collectiblesTotalText.text = collectiblesTotal.ToString();
	}

    //Counts a collectable towards the total and updates the UI
    public void CollectibleCollected()
	{
		collectiblesCollected += 1;
		collectibleCollectedText.text = collectiblesCollected.ToString();
	}

    //Timer bar that decreases and stops the level when it runs out
    //Time.deltaTime: https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
    //Video on Bar Timer: https://www.youtube.com/watch?v=bcvLM_riVuw
    public void TimerDecrease()
    {
        if (timeRemaining >= 0)
        {
            if (gm.GetComponent<GameManager>().winScreen.activeSelf || gm.GetComponent<GameManager>().loseScreen.activeSelf)
            {
                Destroy(timerBar);
            }
            else
            {
                timeRemaining -= Time.deltaTime;
                timerBar.fillAmount = timeRemaining / maxTime;
            }
        }
        else
        {
            Destroy(timerBar);
            gm.GetComponent<GameManager>().TriggerGameOver();
        }
    }
}
