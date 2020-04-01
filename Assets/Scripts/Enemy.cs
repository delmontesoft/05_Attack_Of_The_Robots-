using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] int hitPoints = 50;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        //todo add scoreboard
        //todo tower variable damage per hit

        hitPoints = hitPoints - 1;      
        hitFX.Play();
    }

    private void KillEnemy()
    {
        GameObject deathFXInstance = Instantiate(deathFX, transform.position, Quaternion.identity, transform.parent);
        Destroy(deathFXInstance, 2f);
        Destroy(gameObject);
    }

    IEnumerator FollowPath(List<Waypoint> path)     //IEnumerator converts the method to a co-rutine (runs along other code)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);        // yield return pause the execution of the corutine for WaitForSeconds seconds
        }
    }
}
