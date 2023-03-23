using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowGrid : MonoBehaviour
{
    private GridSystem gridSys;
    public bool isOnGrid;
    // Start is called before the first frame update
    void Start()
    {
        gridSys = FindObjectOfType<GridSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGrid)
        {
            transform.position = gridSys.grid_mousePosition + new Vector3(0, 0.3f, 0);
        }

    }
}
