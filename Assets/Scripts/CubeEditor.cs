using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]     //to add functionality executable on the edit mode
[SelectionBase]         //to force the selection of the object this script is attached to when selecting any part of the object on the editor
public class CubeEditor : MonoBehaviour
{
    [SerializeField][Range(1,20)] int gridSize = 10;

    TextMesh textMesh;

    // Update is called once per frame
    void Update()
    {
        SnapCubeToGrid();

        UpdateCoordText();
    }

    private void SnapCubeToGrid()
    {
        Vector3 snapPosition;

        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.y = 0f;                                                            //keep the cube always on the ground
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = snapPosition;
    }

    private void UpdateCoordText()
    {
        string coordText;

        coordText = "(" + transform.position.x / gridSize + "," + transform.position.z / gridSize + ")";


        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = coordText;

        transform.name = coordText;
    }
}
