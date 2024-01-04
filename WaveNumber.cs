using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveNumber : MonoBehaviour
{
    //to ensure that it uses the correct peice of text 
    public TextMeshProUGUI WaveNumberText;
    
    // Update is called once per frame
    void Update()
    {
        // this function will update the piece of text each frame and when the wave moves to the next wave it will display which wave the player is currently on
        ZombieSpawner WNumber = FindObjectOfType<ZombieSpawner>();
        WaveNumberText.text = WNumber.CWave.ToString();          

    }
}
