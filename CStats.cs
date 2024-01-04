using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStats : MonoBehaviour
{
    // this variable will be used to track the health of the player
    public int Health;
    // This varaible is used to limit the maximum ammount of health that the player can have 
    public int MHealth;
    //this varaible will be used to initiate the model dying
    protected bool isDead;


    private void Start()
    {
        //reference to the character stat below in the code and will run immediately as soon as the game starts
        CharacterStat();
    }

    public virtual void CheckHealth()
    {
        // this function states that if the health level of player reaches zero then it will play the die function referenced below
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
        // this function will correct the health to the maximum amount of health that the player can gain if it goes over the maximum amount that the player should have
        if (Health >= MHealth)
        {
            Health= MHealth;
        }

    }

    //this will set the health to whatever it is in the function CheckHealth
    public void SHealthTo(int HealthSetTo)
    {
        Health= HealthSetTo;
        CheckHealth();
    }

    //This fucntion will play when the character is dead, to determine whether the character is dead or not will be defined in other functions
    public bool IsDead()
    {
        return isDead;
    }

    // this function calcualtes the health for the model after it has taken any damage
    public void DamageTaken (int damage)
    {
        int HAfterDamage = Health - damage;
        SHealthTo(HAfterDamage);
    }


    // when the die function is referenced/activated it will lead to the iddead variable to be true, this variable activates if the player is at 0 health
    public virtual void Die()
    {
        isDead = true;
    }
  
    // This makes it so the player can never go above the maximum amount of health as the character stat always sets the amount of health to the maximum amount of health
    public void Update()
    {
        if (Health > 100) 
        {
            CharacterStat();
        }
    }
    

    // this is what the stats of the model will be/how much health they can have
    public virtual void CharacterStat()
    {
        //max health is equal to 100 so health will automatically be set to 100 
        MHealth = 100;
        SHealthTo(MHealth);
        isDead= false;
    }

}
// Reference: SingleSaplingGames https://www.youtube.com/watch?v=XKUdmz5VFbY (Not including UI element) 

