using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    //returns no value 
    private NavMeshAgent agent = null;
    // determines the position of the game object that will be placed here, in this case it will be the player model 
    [SerializeField] private Transform Player;
    // the distance where the model stops
    [SerializeField] private float stoppingDistance = 2.5f;
    private float TOfLastAttack = 0;
    
    //starts with no value returning the animation/no animation
    private Animator Animation = null;
    private ZStats stats = null;


    private void Start()
    {
        //these are the components within the zombie model and gets these components 
        agent = GetComponent<NavMeshAgent>();
        Animation = GetComponentInChildren<Animator>();
        stats = GetComponent<ZStats>();
    }

    private void Update()
    {
        //every frame it playes the move to player function listed below
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        // this sets the end destination of the model to the players position 
        agent.SetDestination(Player.position);
        Animation.SetFloat("Speed", 1.3f, 0.5f, Time.deltaTime);
        
        // this chracterises how far the zombie model is from the player model with specific float variables
        float distancetoPlayer = Vector3.Distance(transform.position, Player.position);

        // if the zombie is within the range of the player model then the rotate to player function will play 
        if(distancetoPlayer <= stoppingDistance)
        {
            Animation.SetFloat("Speed", 0f);
            RotateToPlayer();
                       
            // Attack
            // will wait for 1.5 seconds until the attack will restart
            if(Time.time >= TOfLastAttack + stats.AttackS)
            {
                TOfLastAttack= Time.time;
                // this will determine what happens to the players stats and the AP function will play which is specified below
                CStats targetStats = Player.GetComponent<CStats>();
                APlayer(targetStats);
            }
           
        }
        
    }
    private void RotateToPlayer()
    {
        // tracks the player position and looks at the player position instead of the actual player
        Vector3 PlayerPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        transform.LookAt(PlayerPosition);
    }

    // This is the function that is responsible for attacking the player 
    private void APlayer(CStats statsToDamage)
    {
        // when the player attacks the player the animation for the attack will play and make the program calculate any damages by using the function in the CStats class
        Animation.SetTrigger("Attack");
        stats.DDamage(statsToDamage);
    }

}
// Reference: SingleSaplingGames, https://www.youtube.com/watch?v=LIn2jOyOTKQ (Zombie Movement) https://www.youtube.com/watch?v=I3sY3I_f5_o (animations) https://www.youtube.com/watch?v=zzOi8wq3T4E&t=532s (Zombie Attack) https://www.youtube.com/watch?v=rb7in84zxmY&t=1s (Killing Zombies) 