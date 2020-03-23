using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Vector2Int[] searchDirections = 
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadWaypoints();
        SetStartAndEndColor();
        Pathfind();
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);
         
        while ((queue.Count > 0) && isRunning)
        {
            Waypoint searchWaypoint = queue.Dequeue();
            searchWaypoint.isExplored = true;
            print("Searching @ " + searchWaypoint);     //TODO remove later
            HaltIfEndFound(searchWaypoint);
            ExploreNeighbour(searchWaypoint);
        }

        print("Finished pathfinding?");
    }

    private void HaltIfEndFound(Waypoint searchWaypoint)
    {
        if (searchWaypoint == endWaypoint)
        {
            print("Searching from endpoint, so we stop the search");    //TODO remove later
            isRunning = false;
        }
    }

    private void ExploreNeighbour(Waypoint currentSearchWaypoint)
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in searchDirections)
        {
            Vector2Int neighbourCoordinates = currentSearchWaypoint.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);

            } catch
            {
                // do nothing for now
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

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if (!neighbour.isExplored && !queue.Contains(neighbour))
        {
            queue.Enqueue(neighbour);
            neighbour.SetTopColor(Color.cyan); //TODO remove later 
            print("Adding " + neighbour + " to queue");     //TODO remove later
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
