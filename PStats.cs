using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStats : CStats
{    
    private HealthHud healthHud;

    public void Start()
    {
        healthHud = GetComponent<HealthHud>();
        //this will set the players health to 100 when the game starts
        CharacterStat();
    }
       

}
// // Reference: SingleSaplingGames https://www.youtube.com/watch?v=XKUdmz5VFbY (not including UI element) 