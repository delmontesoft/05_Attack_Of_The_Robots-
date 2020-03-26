using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        objectToPan.LookAt(targetEnemy);

        if (enemyInRange())
        {
            ActivateTurretFire(true);
        }
        else
        {
            ActivateTurretFire(false);
        }

    }

    private bool enemyInRange()
    {
        return true;    //todo detect if enemy is in range of fire
    }

    private void ActivateTurretFire(bool isActive)
    {
        var emissionModule = GetComponentInChildren<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
