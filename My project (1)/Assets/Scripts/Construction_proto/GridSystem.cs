using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public Transform prefab_gridCell;
    public Transform onMousePrefab;


    public Vector3 grid_mousePosition;
    public Vector3 recent_mousePosition;

    public LayerMask mask;

    [SerializeField] int height;
    [SerializeField] int width;

    public int magnification_cell;

    Node[,] nodes;
    RaycastHit enterHit;

    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        GetGridPositonOnMousePositon();
    }

    void GetGridPositonOnMousePositon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out enterHit, Mathf.Infinity, mask))
        {
            recent_mousePosition = enterHit.point;
            grid_mousePosition.y = 0;
            grid_mousePosition = Vector3Int.RoundToInt(recent_mousePosition / magnification_cell) * magnification_cell;
            foreach (var node in nodes)
            {
                if (node.cellPosition == grid_mousePosition && node.isPlaceable)
                {
                    if (Input.GetMouseButtonUp(0) && onMousePrefab != null)
                    {
                        node.isPlaceable = false;
                        onMousePrefab.GetComponent<ObjFollowGrid>().isOnGrid = true;
                        onMousePrefab.position = node.cellPosition;
                        onMousePrefab = null;
                    }

                }

            }
        }
    }
    private void CreateGrid()
    {
        nodes = new Node[width, height];
        //var name = 0;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition
                    = new Vector3(transform.position.x + i * magnification_cell, transform.position.y, transform.position.z + j * magnification_cell);
                Transform obj = Instantiate(prefab_gridCell, worldPosition, Quaternion.identity);

                obj.name = "Cell [" + i + "," + j + "]";
                nodes[i, j] = new Node(true, worldPosition, obj);
                obj.transform.parent = gameObject.transform;
                obj.gameObject.layer = LayerMask.NameToLayer("Grid");
            }
        }
    }

}
