using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contruction_DragAndDrop : MonoBehaviour
{
    public GameObject objToPlace;
    public LayerMask mask;
    public float lastPosY;
    public Vector3 mousePos;

    public Material matGrid, matDefault;
    public bool isDragging;

    RaycastHit hit;
    Renderer rend;

    void Start()
    {
        rend = GameObject.Find("Ground").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                int posX = (int)Mathf.Round(hit.point.x);
                int posZ = (int)Mathf.Round(hit.point.z);

                objToPlace.transform.position = new Vector3(posX, lastPosY, posZ);
            }
            else
            {
                isDragging = false;
            }

            if(isDragging)
            {
                rend.material = matGrid;
            }else if (!isDragging)
            {
                rend.material = matDefault;
            }
        }
    }
}
