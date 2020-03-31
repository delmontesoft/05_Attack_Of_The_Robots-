using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;

    bool isStartWaypoint = false;
    bool isEndWaypoint = false;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            print("Mouse is over " + gameObject.name + " and available");
        } else if (Input.GetMouseButtonDown(0))
        {
            print("Mouse is over " + gameObject.name + " but is not available");
        }
        
    }
}
