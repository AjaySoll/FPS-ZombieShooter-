using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthHud : MonoBehaviour
{
    // Reference to the health Hud text
    public TextMeshProUGUI HealthAmmountText;
    void Update()
    {
        //this will get hwo much health thatthe player has from the CStats class that we had made due to the Player stats also referencing the CStats 
        CStats currenthealth = GetComponent<CStats>();
        //This will display how much health the player has currenty and also show the maximum amount of health that the player can have 
        HealthAmmountText.text = currenthealth.Health + " / " + currenthealth.MHealth;
    }
}
