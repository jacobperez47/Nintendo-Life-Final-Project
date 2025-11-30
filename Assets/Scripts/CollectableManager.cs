using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{

    public GameObject majorUnlock;
    
    
    [CanBeNull]
    public List<Accessory_Script> collectables;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }

    bool checkCollectables()
    {
        foreach (var item in collectables)
        {
            if (!item.isCollected)
            {
                return false;
            }
        }

        return true;
    }
    

    public void checkCount()
    {
        if (checkCollectables())
        {
            majorUnlock.SetActive(true);
        }
        
    }
}