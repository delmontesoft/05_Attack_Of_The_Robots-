using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    [SerializeField] int healthDecrease = 1;

    private void OnTriggerEnter(Collider collider)
    {
        if (playerHealth >0)
        {
            playerHealth = playerHealth - healthDecrease;

        } else
        {
            //TODO do something when player loses
            print("You lose!");
        }
        
    }
}
