using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;

    public Vector3 smoothMousePosition;
    Vector3 mousePosition;

    [SerializeField] int height;
    [SerializeField] int width;

    Node[,] nodes;
    Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        plane = new Plane(Vector3.up, transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
    }
    private void CreateGrid()
    {
        nodes = new Node[width, height];
        //var name = 0;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell [" + i + "," + j + "]";
                nodes[i, j] = new Node(true, worldPosition, obj);
            }
        }
    }
    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(plane.Raycast(ray,out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);

            foreach (var node in nodes)
            {
                if(node.cellPosition == mousePosition && node.isPlaceable)
                {
                    if(Input.GetMouseButtonUp(0) && onMousePrefab != null)
                    {
                        node.isPlaceable = false;
                        onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true;
                        onMousePrefab.position = node.cellPosition + new Vector3(0, 0.5f, 0);
                        onMousePrefab = null;
                    }
                }
            }
            //print(mousePosition);
        }
    }
    public void OnMouseClickOnUI()
    {
        if (onMousePrefab == null)
        {
            onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
        }
}

}


public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}
