using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // alows there to be a game object placed and that game object will inherit these functions
    [SerializeField] Slider volumeslider;

    // this will either load the volume value that the volume slider was set to previously or set it to 1 by default if the volume has never been changed 
    private void Start()
    {
        if (!PlayerPrefs.HasKey("AudioManager"))
        {           
            PlayerPrefs.SetFloat("AudioManager", 1);
            Load();
        }
    }

    public void VolumeChange()
    {
        // this changed the volume level within the game and changed all audiosources within the game
        AudioListener.volume = volumeslider.value;
        Save();
    }

    private void Save()
    {
        // this saves the value set in the vomlume slider 
        PlayerPrefs.SetFloat("AudioManager", volumeslider.value);
    }

    private void Load()
    {
        // this will get the value of the volume slider from the save functon 
        volumeslider.value = PlayerPrefs.GetFloat("AudioManager");
    }

  
}
