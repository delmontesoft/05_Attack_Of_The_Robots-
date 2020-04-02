using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] int damagePerHit = 1;
    [SerializeField] int fireRange = 30;

    public Waypoint baseWaypoint;
    Transform targetEnemy;

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
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        float shortestDistance = 10000;

        foreach (Enemy enemy in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position , gameObject.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }

        return targetEnemy;
    }

    private bool enemyInRange(Transform targetEnemy)
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.  transform.position);
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
