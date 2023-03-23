using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    CameraHolder cHolder;

    [SerializeField]
    private Slider bar_SP;
    // Start is called before the first frame update
    void Start()
    {
        cHolder = FindObjectOfType<CameraHolder>();
    }

    public void SetSP(float sp)
    {
        bar_SP.value = sp/ playerController.stamina;
    }
    // Update is called once per frame
    void Update()
    {
        if(!cHolder.seePlayer)
        {
            bar_SP.gameObject.SetActive(false);
        }
        else
        {
            bar_SP.gameObject.SetActive(true);

        }
        SetSP(playerController.cur_stamina);

    }
}
