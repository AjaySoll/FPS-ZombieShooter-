using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // varaible where deathscreen game object will be placed 
    public GameObject Deathscreen;
    // variable where player model will be placed 
    public GameObject PlayerModel;

    // bool to say if player is dead or not, static so it can be used in other scripts 
    public static bool ISDead;

    // when the game starts the player is not dead 
    private void Start()
    {
        // will set bool to false and theefore not active 
        ISDead = false;
    }

    // if the player is not dead then ISDead will be set to false, if the player is dead ISDead will be set to true 
    private void Update()
    {
        // if the character is not dead, the death screen will not appear
        if (!ISDead)
        {
            Deathscreen.SetActive(false);
        }
        // if the charcter is set to "isdead" (this will be when the character health reaches zero), the deathcreen menu will appear 
        else
        {
            Deathscreen.SetActive(true);
        }

        // if the player is at 0 health the dead function will play 
        if (PlayerModel.GetComponent<CStats>().Health == 0)
        {
            // this will activate the dead function which has been defined 
            Dead();
        }
    }

   

    // when this function is played the deathscreen will pop up due to ISDead being true and time will freeze, the player cursor will also be unlocked and visable 
    public void Dead()
    {
        //freezes all animations and game 
        Time.timeScale = 0f;
        // this will set to the iSDead bool to true 
        ISDead = true;
        // this allows the cursor to be seen and allows the user to choose a button to press 
        Cursor.visible = true;
        // this will unlock the cursor and allow the player to mvoe the cursor around to select deathscreen menu buttons 
        Cursor.lockState = CursorLockMode.None;
    }

    // when the main menu button is pressed, this function is activated, the main menu scene will load and the player will not be dead 
    public void MainMenuButton()
    {
        // this will load the main menu scene where the main menu will be displayed 
        SceneManager.LoadScene("MainMenu");
        // this will ensure that the bool function is off so that if the player loads back into the game through the start game button the player will not be dead
        ISDead = false;
    }

    
    
}

