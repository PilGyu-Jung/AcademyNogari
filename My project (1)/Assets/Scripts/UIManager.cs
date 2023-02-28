using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    private Slider bar_SP;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void SetSP(float sp)
    {
        bar_SP.value = sp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
