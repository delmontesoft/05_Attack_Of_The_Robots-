using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 3;

    Queue<Tower> towerRingBuffer = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towerRingBuffer.Count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity, transform);

        newTower.baseWaypoint = baseWaypoint;
        newTower.baseWaypoint.isPlaceable = false;
        towerRingBuffer.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newbaseWaypoint)
    {
        var existingTower = towerRingBuffer.Dequeue();

        existingTower.baseWaypoint.isPlaceable = true;
        existingTower.baseWaypoint = newbaseWaypoint;
        existingTower.baseWaypoint.isPlaceable = false;
        existingTower.transform.position = newbaseWaypoint.transform.position;
        towerRingBuffer.Enqueue(existingTower);
    }
}
