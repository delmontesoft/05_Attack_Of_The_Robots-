using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());

    }

    IEnumerator FollowPath()     //IEnumerator converts the method to a co-rutine (runs along other code)
    {
        print("Starting Patrol");

        foreach (Waypoint waypoint in path)
        {
            print("Visiting " + waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);        // yield return pause the execution of the corutine for WaitForSeconds seconds
        }

        print("Ending patrol");
    }

}
