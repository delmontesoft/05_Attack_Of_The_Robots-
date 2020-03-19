using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField][Range(1,20)] int gridSize = 10;

    // Update is called once per frame
    void Update()
    {
        Vector3 snapPosition;

        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.y = 0f;                                                            //keep the cube always on the ground
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = snapPosition;
        
    }
}
