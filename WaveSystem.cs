using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows the wavesystem to be seen in the inspector 
[System.Serializable]
public class WaveSystem
{
    //allows there to be a tittle of what wave each wave is
    public string WaveNumber;
    // holds how many enemies will be spawned in that wave 
    public int EAmount;
    //delays when next zombie can be spawned
    public float DelaySpawn;
    // allows it to assign a zombie type e.g can have different types of zombies 
    public GameObject Zombie;
        
}
