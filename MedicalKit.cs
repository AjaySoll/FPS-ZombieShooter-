using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// reference to CStats class as i will be using varaibles from this class 
public class MedicalKit : CStats
{
    // this is reference to the pickup item, in this case the Medkit will be placed in this variable
    public GameObject pickup01;
    // this is the variable that will hold the player model 
    public GameObject PlayerModel;
    //this is where the pickup text we previously created will be held
    public GameObject pickupText;
    // this is where the cannot pcikup text will be held that we prevoiusly created
    public GameObject cannotpickupText;
    // this will be the amount of health that will be gained when interacted with the object 
    public int Heal;
    // this will track the current health of the player,
    private float CHealth;
    // this is associated to the "Range" game object that was created to monitor if the player is in reach to another component, when this game object collides with another game object it will activate
    public bool Range;
    // this will hold a chosen sound that will play when the player interacts with the object attached to this game object
    public AudioSource HealthPickup;

    // this function will be activated when the player is within range of the object 
    private void OnTriggerEnter(Collider other)
    {
        // if the game object that is tagged as "range" then the code will play
        if (other.gameObject.tag == "Range")
        {
            // the bool value of rain will activate as true and the pickup text will play
            Range = true;
            pickupText.SetActive(true);
        }
    }
    // this function will be activated when the player is no longer in the range of the object/player looks away from the object  
    private void OnTriggerExit(Collider other)
    {
        // if the game object that is tagged as "range" then the code will play
        if (other.gameObject.tag == "Range")
        {
            // the bool value of rain will activate as false and the the texts will not play
            Range = false;
            pickupText.SetActive(false);
            cannotpickupText.SetActive(false);
        }
    }

    public void Start()
    {
        // gets the health level from the health variable in the PStats class 
        CHealth = PlayerModel.GetComponent<CStats>().Health;
        // wont be played automatically as soon as game starts
        // texts will be set to false until the player has fufilled the condion which is in the ontrigger enter or exit function 
        cannotpickupText.SetActive(false);
        pickupText.SetActive(false);
        Range= false;
    }

    public void Update()
    {
        // if the game is paused the functions will not work
        if (!PauseMenu.IsPaused)
        {
            // if the game object range is colliding is true and the player presses E, also if the player is lower than 100 health then this function will player 
            if (Range && Input.GetKeyDown(KeyCode.E) && PlayerModel.GetComponent<CStats>().Health < 100)
            {
                // the range will be set to false and the player will no longer be in range
                Range = false;
                //this will play the helath pickup sound 
                HealthPickup.Play();
                // this will add a specific amount of health depending on what the heal value is worth
                CHealth = PlayerModel.GetComponent<CStats>().Health += Heal;
                // the text to pickup the object will no longer be displayed 
                pickupText.SetActive(false);
                // these two make the object to disappear and no longer be seen or interacted with the player
                pickup01.GetComponent<BoxCollider>().enabled = false;
                pickup01.GetComponent<MeshRenderer>().enabled = false;

            }
            // if the players health is already at 100 the cannot pikcup text will player stating that they cannot heal
            else if (Range && Input.GetKeyDown(KeyCode.E) && PlayerModel.GetComponent<CStats>().Health == 100)
            {
                pickupText.SetActive(false);
                cannotpickupText.SetActive(true);
            }
        }
       


    }
}
// reference: User1Productions https://www.youtube.com/watch?v=TssubnQzW1E&t=388s