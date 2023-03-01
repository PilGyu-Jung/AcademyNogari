using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField]
    private Slider bar_SP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSP(float sp)
    {
        bar_SP.value = sp/ playerController.stamina;
    }
    // Update is called once per frame
    void Update()
    {
        SetSP(playerController.cur_stamina);
    }
}
