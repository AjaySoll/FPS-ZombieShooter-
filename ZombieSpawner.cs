using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // public enum configures a list using assgined variables in brackets 
    public enum SpawnState { Spawning, Waiting, Counting}
    // creates a variables that holds the current wave and references it to the WaveSystem class and the array is named at waveSystems in this class
    [SerializeField] private WaveSystem[] waveSystems;
    //time inbetween waves 
    [SerializeField] private float BeforeNextWave = 2f;
    // countdown fo next wave
    [SerializeField] private float WaveCountdown = 0;

    // used to hold the EndScreen game object
    public GameObject EndScreen;

    private SpawnState state = SpawnState.Counting;
    // current wave that is playing
    public int CWave;

    //determines position of the ZSpawners/allows the input of this variable and inputs the game object where the zombies will spawn from
    [SerializeField] private Transform[] ZSpawners;
    [SerializeField] private List <CStats> Zombielist;
    

    private void Start()
    {
        // waves will start spawning after 2 seconds
        WaveCountdown = BeforeNextWave;
        CWave = 0;
    }


    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            //checks if all Zombies are dead
            //if Zombies are still left then it will keep checking if they are dead 
            // limits the amount of enemies that spawn to the enemies amount assigned per wave 
            if (!ZombieDead())
                return;
            else 
                // this completees the wave and innitiates the next wave 
                WaveComplete();

        }

        //if thw countdown reaches zero this function will play 
        if (WaveCountdown <= 0)
        {
            //spawns the zombies 
            // will make the spanwing happen once at a time
            if (state != SpawnState.Spawning)
            {
                //will allow the control of each wave that is playing 
               StartCoroutine(WaveSpawn(waveSystems[CWave]));
            }
        }
        // this will countdown the until it reaches zero or below zero so that the above function can activate
        else
            WaveCountdown -= 1* Time.deltaTime;
    }

    //hanldles spawning the waves and zombies 
    private IEnumerator WaveSpawn(WaveSystem waveSystem)
    {
        // if this is true it will not start another sapwning cycle 
        state= SpawnState.Spawning;
        // creates a variable I. if I is less than enemy amount the code is run an 1 is added to i (through the i++ function) and checks again, this code is ran until i is greater than the wavesystem variable
        // this part of the code makes it so the zombies dont all spawn at once
        for (int i = 0;  i < waveSystem.EAmount; i++)
        {
            //spawns the zombie 
            ZombieSpawn(waveSystem.Zombie);
            // this function will play when the set amount of time in the variable delay spawn is complete
            yield return new WaitForSeconds(waveSystem.DelaySpawn);
        }
        // this will set the state of the zombies to waiting and not spawn in any zombies 
        state = SpawnState.Waiting;
        // this is used in IEnumerator functions more commonly and acts as a return statement without intially returning a value, instead it just specifies that this iterator has come to an end
        yield break;
    }    


    // loop through all the spawners and spawn a zombie at random of the spawners 
    private void ZombieSpawn(GameObject Zombie)
    {
        // it will give a random number between 1 and the amount of spawners that we have in order to generate what spawner to use e.g if the random value was 5 it will get spawner 5 from the list we created prevoiusly named ZSpawners
        int RInt = Random.RandomRange(1, ZSpawners.Length);
        // this determines the rotation and position of the zombie spawners and assigns the randomZSpawner variable with the value of the RInt previously created 
        Transform randomZSpawner = ZSpawners[RInt];
        // this handles the spawning of the zombie, it gets the zombie, the position and rotation of the random spawner spawners chosen from RInt function,  
        GameObject newZombie = Instantiate(Zombie, randomZSpawner.position, randomZSpawner.rotation);
        // creates a new zombie from using CStats 
        CStats newZombieStats = newZombie.GetComponent<CStats>();
        // will add the zombie to a enemey list 
        Zombielist.Add(newZombieStats);
    }

    // this function checks if the enemies are dead
    private bool ZombieDead()
    {
        int i = 0;
        foreach(CStats Zombie in Zombielist)
        {
            // if the enemy is dead it will continue checking for enemies
            if (Zombie.IsDead())
                i++;
            else 
                return false;
        }
        //if the function is done for each zombie in the zombie list it will return as true which will mean all the eneimes are dead 
        return true;
    }

    //when the wave is complete it will move onto the next wave
    private void WaveComplete()
    {
        //when the user kills all the zombies the log will return waves completed, front end to be added soon 
        Debug.Log("Wave Completed");

        state = SpawnState.Counting;
        WaveCountdown = BeforeNextWave;

        //when all waves complete, the end screen menu will appear, all animations will also pause and the cursor will be visable to the player
        if (CWave + 1 > waveSystems.Length - 1)
        {
           EndScreen.SetActive(true);
           Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // if the waves are not complete it will keep adding +1 and move onto the next wave and the end screen will not be displayed
        else
        {
            CWave++;
            EndScreen.SetActive(false);
        }

    }

}
// Reference: SingleSaplingGames https://www.youtube.com/watch?v=uODu1XkzVFw