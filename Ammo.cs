using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // this allows this code to work for the text that is attached to this class
    public TextMeshProUGUI AmmoInfoText;
  
    // Update is called once per frame
    void Update()
    {
        // this is used to display the ammo count for the user interface and track how much ammo the user has 
       Gun currentGun = FindObjectOfType<Gun>();
        AmmoInfoText.text = currentGun.currentAmmo + " / " + currentGun.ReserveAmmo;

    }
}

//GDTitans