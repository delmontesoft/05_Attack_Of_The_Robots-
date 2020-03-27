using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] int damagePerHit = 1;
    [SerializeField] int fireRange = 30;

    // Update is called once per frame
    void Update()
    {
        Transform closestEnemy = findClosestEnemy();
        if (closestEnemy != null && enemyInRange(closestEnemy))
        {
            objectToPan.LookAt(closestEnemy);
            ActivateTurretFire(true);
        }
        else {
            ActivateTurretFire(false);
        }

    }

    private Transform findClosestEnemy()
    {
        //todo change targetEnemy for the closest enemy between all enemies
        return targetEnemy;
    }

    private bool enemyInRange(Transform targetEnemy)
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
        if (distanceToEnemy <= fireRange)
        {
            return true;
        }
        else {
            return false;
        }
        
    }

    private void ActivateTurretFire(bool isActive)
    {
        var emissionModule = GetComponentInChildren<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
