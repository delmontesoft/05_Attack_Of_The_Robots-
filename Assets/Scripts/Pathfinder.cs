using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Vector2Int[] allowedDirections =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        LoadWaypoints();
        SetStartAndEndColor();
        ExploreNeighbour();
    }

    private void ExploreNeighbour()
    {
        foreach (Vector2Int direction in allowedDirections)
        {
            Vector2Int neighbourPos = startWaypoint.GetGridPos() + direction;
            print("Exploring " + neighbourPos);
            if (grid.ContainsKey(neighbourPos))
            {
                grid[neighbourPos].SetTopColor(Color.cyan);
            }
                
        }
    }

    private void SetStartAndEndColor()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadWaypoints()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping waypoint @ " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
