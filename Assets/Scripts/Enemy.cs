using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem deathFXPrefab;
    [SerializeField] ParticleSystem selfDestroyFXPrefab;
    [SerializeField] ParticleSystem hitFXPrefab;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip selfDestroySFX;
    [SerializeField] float movementDelay = 2f;
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
            KillEnemy(true);
        }
    }

    private void ProcessHit()
    {
        //TODO tower variable damage per hit

        hitPoints = hitPoints - 1;
        GetComponent<AudioSource>().PlayOneShot(hitSFX);
        hitFXPrefab.Play();
    }

    private void KillEnemy(bool wasKilledByPlayer)
    {
        ParticleSystem deathFX;
        if (wasKilledByPlayer)
        {
            
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
            deathFX = Instantiate(deathFXPrefab, transform.position, Quaternion.identity, transform.parent);
        }
        else
        {
            AudioSource.PlayClipAtPoint(selfDestroySFX, Camera.main.transform.position);
            deathFX = Instantiate(selfDestroyFXPrefab, transform.position, Quaternion.identity, transform.parent);
        }

        Destroy(deathFX.gameObject, deathFX.main.duration);
        Destroy(gameObject);
    }

    IEnumerator FollowPath(List<Waypoint> path)     //IEnumerator converts the method to a co-rutine (runs along other code)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementDelay);        // yield return pause the execution of the corutine for WaitForSeconds seconds
        }

        //when reaches the end explode and do damage to player
        KillEnemy(false);
    }
}
