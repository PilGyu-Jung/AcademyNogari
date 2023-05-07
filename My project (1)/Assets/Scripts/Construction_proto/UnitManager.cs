using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> unitList = new List<Unit>();
    public GameObject prefab_HpBar = null;
    public GameObject canvas;
    [Range(1, 10)]
    public float height_hpBar;


    private static UnitManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        canvas = FindObjectOfType<Canvas>().gameObject;
    }

    public static UnitManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
}
