using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    List<Waypoint> path = new List<Waypoint>();
    Waypoint currentSearchWaypoint;
    bool isRunning = true;

    Vector2Int[] searchDirections = 
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        LoadWaypoints();
        SetStartAndEndColor();
        BreadthFirstSearch();
        CreatePath();

        return path;
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

    private void SetStartAndEndColor()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
         
        while ((queue.Count > 0) && isRunning)
        {
            currentSearchWaypoint = queue.Dequeue();
            currentSearchWaypoint.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbour();
        }

        print("Finished pathfinding?");
    }

    private void HaltIfEndFound()
    {
        if (currentSearchWaypoint == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbour()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in searchDirections)
        {
            Vector2Int neighbourCoordinates = currentSearchWaypoint.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if (!neighbour.isExplored && !queue.Contains(neighbour))
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = currentSearchWaypoint;
        }
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint exploredFrom = endWaypoint.exploredFrom;

        while (exploredFrom != startWaypoint)
        {
            path.Add(exploredFrom);
            exploredFrom = exploredFrom.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();

    }
}
