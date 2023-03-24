using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foothold_Construction : MonoBehaviour
{
    CameraHolder cameraHolder;
    GridSystem gridSys;
    public Image ui_Construction;
    public Transform building1;
    public GameObject gridObject;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cameraHolder.seePlayer = false;
            ui_Construction.gameObject.SetActive(true);
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Grid");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraHolder.seePlayer = true;
            ui_Construction.gameObject.SetActive(false);
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Grid"));

        }

    }

    public void OnConstructButtonClick()
    {
        if(gridSys.onMousePrefab == null)
        {
            gridSys.onMousePrefab = Instantiate(building1, gridSys.grid_mousePosition, Quaternion.identity);
        }
    }

    void Start()
    {
        cameraHolder = FindObjectOfType<CameraHolder>();
        gridSys = FindObjectOfType<GridSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
