using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] int hitPoints = 50;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        //scoreBoard.scoreHit(scorePerHit); //todo add scoreboard

        //GameObject hitFXInstance = Instantiate(hitFX, transform.position, Quaternion.identity);
        //hitFXInstance.transform.parent = parent;
        hitPoints = hitPoints - 1;
    }

    private void KillEnemy()
    {
        GameObject deathFXInstance = Instantiate(deathFX, transform.position, Quaternion.identity);
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
