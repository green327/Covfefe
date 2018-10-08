using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button LevelName;
    public string LevelToLoad;
    // Use this for initialization
    void Start()
    {

    }

   public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
