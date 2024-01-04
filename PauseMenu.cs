using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // where the pausemenu will be referenced
    public GameObject Pausemenu;
    // allows it to be accessed by other classes 
    public static bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        // pause menu will not be displayed when game starts 
        Pausemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if the user presses P the pause menu will appear and if pressed again the game will resume  
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
       

    }

    public void Pause()
    {
        // this activates the pause menu 
        Pausemenu.SetActive(true);
        //freezes all animations and game 
        Time.timeScale = 0f;
        IsPaused= true;
        // this allows the cursor to be seen and allows the user to choose a button to press 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        // this closes the pause menu and resumes the game 
        Pausemenu.SetActive(false);
        //unfreezes all animations and game 
        Time.timeScale = 1f;
        IsPaused= false;
        // hides the cursor and locks it in the middle of the screen 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenuButton()
    {
        // when this function is run it will load the Main Menu scene and close the pause menu scene 
        // Pause game menu is closed to ensure that the pause menu will not be seen if the user presses start game in the main menu when opened
        SceneManager.LoadScene("MainMenu");
        IsPaused= false;
    }
    
}

// reference Bmo https://www.youtube.com/watch?v=9dYDBomQpBQ