using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//reference to CStats because some varaibles will be used from the CStats class
public class ZStats : CStats
{
    // this variable will hold the amount of damage the Zombie can do 
    [SerializeField] private int D;
    // this variable will hold the attack speed of the Zombie 
    public float AttackS;
    // this will determine whether the zombie can attack or not 
    [SerializeField] private bool CAttack;

    
    //this wil get the characte stat function from the CStats class and apply it to the zombie 
    private void Start()
    {
        CharacterStat();
    }
   
    // this function is responsible for handling the damage functions of the zombie and taking away the value of health by the value on D 
    public void DDamage(CStats statsToDamage)
    {
        statsToDamage.DamageTaken(D);
    }

    //when the die function is played the zombie game object will be destroyed 
    public override void Die()
    {
        base.Die();        
        Destroy(gameObject);
    }

    // this function inherits the character stats class and its functions, in the scene view i will change the health, max health and attack speed for the zombie manually, this also allows there to be duplicate zombies with different stats 
    public override void CharacterStat()
    {
        // this will set the current health of the zombie to the max health and the zombie will be alive
        SHealthTo(MHealth);
        isDead = false;

        //will allow the zombie to attack
        CAttack = true;
    }
}
//Reference: SingleSaplingGames https://www.youtube.com/watch?v=38_sdVDCyuY