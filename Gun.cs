using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    // determines the position of the camera
    public Transform fpsCam;
    // These are all the components related to the gun

    // Determines how far the raycast travels
    public float range = 20;
    // Deternines how much of an impact the bullets make and how far the object shot at will move
    public float impactForce = 150;
    // determines how fast the bullets are released from the gun 
    public int fireRate = 10;
    // determines how long again a bullet can fire again each time a bullet is fired
    private float nextTimeToFire = 0;

    //varaible that will hold how much damage the gun will do 
    public int damage;

    // allows there to be an audiosource and in this audiosource the variable will be named zombie hit 
    public AudioSource ZombieHit;
    public Animator animator;

    // This allows there to be a particle system effect to be inputted which will activate when weapon is fired
    public ParticleSystem muzzleFlash;
    // This allows there to be an object left behind when the gun is fired and hits an object on the map
    public GameObject impactEffect;
    

    // This variable controls the current ammo that the gun has in the magazine, this variable updates everytime a shot is fired and when reload function is done
    public int currentAmmo;
    // This variable controls the maximum amount of bullets the gun magazine can have
    public int maxAmmo = 30;
    // This variable controls how much ammo is in the reserve and not currently in the guns magazine/how many bullets they have left in total not including the current magazine
    public int ReserveAmmo = 300;

    // This public class controls how long it takes for the reload to be completed 
    public float reloadTime = 3f;
    // this private class checks if we are reloading or not
    private bool isReloading;

    // allows there to be an audiosource and in this audiosource the variable will be named GunReload
    public AudioSource GunReload;

    // Creates the variables shoot and functions/information is added below 
    InputAction shoot;
    // Start is called before the first frame update
    void Start()
    {
        
        // Shoot function is binded to the left mouse button and activates every time leftmouse button is hrld down or tapped
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        // This enables the gun to shoot and acively looks for inputs and conducts responses when that input is pressed 
        shoot.Enable();

        // When the game starts the maximum amount of ammo that the gun can have will be loaded into the gun by default
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Death.ISDead)
        {
            if (!PauseMenu.IsPaused)
            {
                // this function states that if the amount of ammo is at 0 and the amount of ammo is in the reserve is also at zero then the player can no longer shoot or reload
                if (currentAmmo <= 1 && ReserveAmmo <= 1)
                {
                    animator.SetBool("isShooting", false);
                    return;
                }

                //if the player is reloading then it will return to the control method and terminate the function when it is finished, without this the functions wont be reset and nothing will be done after the reload is done
                if (isReloading)
                    return;

                // when the key R is pressed on the keyboard it will initiate the reload function defined further down in the code
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(Reload());
                }

                // This checks whether we are shooting or not, when holding the left mouse button it will return as 1, if it returns as 1 it will lead to it being true 
                bool isShooting = shoot.ReadValue<float>() == 1;
                // if the player is shooting then the animation for shooting will play
                animator.SetBool("isShooting", isShooting);

                // This function acitvates when the value 1 is returned and when it is therefore true 
                // this limits the amount of bullets that the player can fire per second 
                if (isShooting && Time.time >= nextTimeToFire)
                {
                    // the 1f/firerate controls the fire rate
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Fire();
                }

                // if the ammo is at zero and the model is not already reloading then the reload function will play
                if (currentAmmo < 1 && !isReloading)
                {
                    StartCoroutine(Reload());
                }
            }
        }
      
        
    }


    // This is the firing function that invlovles the raycast functions
    private void Fire()
    {
        // this is not defined here as it keeps updating whilst in game so it cannot be set 
        currentAmmo--;
        
        // This allows the code to register any objects that it has hit/going to be hit
        RaycastHit hit;
        // when the raycast function is toggled and activated the muszzle particle effect plays
        muzzleFlash.Play();        
        // Physics raycast is a building function for unity that allows raycasting to happen
        //Fps cam posotin is the position in which the raycast will start 
        // fps cam forward creates an invisible line which is the ray cast and it is aimed towards the front onwards to the camera 
        // the out hit function allows object to be hit by the raycast function and recognises the object which is hit
        // the range function toogles the range of the invisible line and how far the raycast can travel
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, range))
        {
            //if the invisible object returns as true this function will be carried out
            //if the object hit is a rigid body object the net function will play 
            if(hit.rigidbody != null)
            {
                //this adds force to the raycast hit 
                //the hit.normal is the vector which is perpandicular to the object hit, by adding the - to the start is propels the force in the opposite direction to allow the object to move toward the vector in-
                //which the object was shot from and this is multipled by the float number within the impact force varaible 
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                
                // when the gun is shot and hits an object, in the console it will display what was hit 
                Debug.Log(hit.transform.name);
                // if the gun shot hits a game object with the tag name Zombie this function will play
                if (hit.transform.tag == "Zombie")
                {
                    // this will play the audio that is attached to the audio source named as zombiehit                  
                    ZombieHit.Play ();
                    // it will get the component from CStats/function from this class and use the Damage taken class and in particular how much damage is set in the varible prevously created
                    CStats ZombieStats = hit.transform.GetComponent<CStats>();
                    ZombieStats.DamageTaken(damage);
                }
            }

            // These fucntions control the impact effects 
            // Quaternion handles the roations of the game object 
            //The first line will create the roation for the game object 
            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            // This controls when the impact effect disapears 
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            // This allows the impact effect to stick onto the rigit body/any game object if it moves rather than freeze at the point in which the effect first made contact with the object 
            impact.transform.parent = hit.transform;
            // This controls the time it takes to destroy the impact effect and when it is destroyed that one impact effect is erased 
            Destroy(impact, 5);
        }
    }

    // Reload function
    // the IEnumerator ensures that the current process is paused and this process is done, after the process is completed it will resume to the previous process that was occurring
    IEnumerator Reload()
    {
        // if the player is reloading
        isReloading = true;
        // the animation for the reload is played
        animator.SetBool("isReloading", true);
        //the animation will stay until the amout of time is passed previously stated in the reloadTime variable
        yield return new WaitForSeconds(reloadTime);
        // after the set amount of time is passed (time according to reloadTime variable) then the reload animation will stop playing and will transition to whatever the player is doing 
        animator.SetBool("isReloading", false);
        // Plays the reload audio source
        GunReload.Play();
        //when their is more ammo in the reserve ammo slot than the integer value in the max ammo slot then this function will play
        if (ReserveAmmo >=maxAmmo)
        {
            // when the reload is done, the amount of ammo from the reserve is taken away by a default value of 30, and however much ammo is left in the current magazine that the player is using
            ReserveAmmo -= 30 - currentAmmo ;
            // the amount of ammo that the gun will have in the current magazine will alwyays only total up to/equal the maxammo variable which is stated above as 30
            currentAmmo = maxAmmo;
            
        }
        //if there is no more ammo left in the reserves then the gun will be unable to fire and relaod any more bullets
        else
        {
            currentAmmo = ReserveAmmo;
            ReserveAmmo = 0;
        }
        isReloading= false;
    }
}

// reference.GDTitans: https://www.youtube.com/watch?v=2N65j9Ir8qQ&t=8s (Shooting) https://www.youtube.com/watch?v=DuCReVDuAV Animations) https://www.youtube.com/watch?v=smZaCfLIk1g Ammo and Reloading) 
// Reference SingleSaplingGames: https://www.youtube.com/watch?v=rb7in84zxmY&t=1s (Killing Zombies) 