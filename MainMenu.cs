using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton()
    {
        // closes the game/application and in the log says that the game was closed 
        Application.Quit();
        Debug.Log("Game quit");
    }

    public void StartButton()
    {
        // loads the game scene 
        SceneManager.LoadScene("Game");
        // game scene is set to normal speed and resumes any in game functions
        Time.timeScale = 1.0f;
        
    } 
    
}
// coco code https://www.youtube.com/watch?v=RsgiYqLID-U