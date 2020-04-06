using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;

    private void Start()
    {
        healthText.text = playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (playerHealth >0)
        {
            playerHealth = playerHealth - healthDecrease;
            healthText.text = playerHealth.ToString();

        } else
        {
            //TODO do something when player loses
            print("You lose!");
        }
        
    }
}
