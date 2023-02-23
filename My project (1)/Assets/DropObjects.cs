using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropRate:Items
{
    int         dropRate;
}


public class DropObjects : MonoBehaviour
{
    bool dropOn;
    public int dropCoin;

    public ItemDropRate[] itemsDrop;

    // Start is called before the first frame update
    void Start()
    {
        dropOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {

    }
}
