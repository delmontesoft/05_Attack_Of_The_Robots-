using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]     //to add functionality executable on the edit mode
[SelectionBase]         //to force the selection of the object this script is attached to when selecting any part of the object on the editor
[RequireComponent(typeof(Waypoint))]

public class CubeEditor : MonoBehaviour
{
    //[SerializeField][Range(1,20)] int gridSize = 10;  // should not be variable between cubes!

    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapCubeToGrid();

        UpdateCubeLabel();
    }

    private void SnapCubeToGrid()
    {
        transform.position = new Vector3(
            waypoint.GetGridPos().x,
            0f,
            waypoint.GetGridPos().y);
    }

    private void UpdateCubeLabel()
    {
        int gridSize = waypoint.GetGridSize();
        string posText;
        posText = waypoint.GetGridPos().x / gridSize + "," + waypoint.GetGridPos().y / gridSize;

        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = posText;

        transform.name = posText;
    }
}
