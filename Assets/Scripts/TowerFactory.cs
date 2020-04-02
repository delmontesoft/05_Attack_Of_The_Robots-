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

        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        towerRingBuffer.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var existingTower = towerRingBuffer.Dequeue();

        existingTower.baseWaypoint.isPlaceable = true;
        existingTower.baseWaypoint = baseWaypoint;
        existingTower.transform.position = baseWaypoint.transform.position;
        existingTower.baseWaypoint.isPlaceable = false;
        towerRingBuffer.Enqueue(existingTower);
    }
}
