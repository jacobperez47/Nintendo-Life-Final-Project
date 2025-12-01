using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{

    public GameObject majorUnlock;
    
    private bool isUnlocked = false;
    
    [CanBeNull]
    public List<Accessory_Script> collectables;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isUnlocked)
        {
            checkCount();
        }
    }

    bool checkCollectables()
    {
        
        collectables.RemoveAll(item => item == null);
        foreach (Accessory_Script collectable in collectables)
        {
            if (!collectable.isCollected)
            {
                return false;
            }
            
        }

        return collectables.Count == 0;    }
    

    public void checkCount()
    {
        if (checkCollectables())
        {
             isUnlocked = true;
            majorUnlock.SetActive(true);
        }
        
    }
    
    public void RemoveCollectable(Accessory_Script collectedItem)
    {
      
            collectables.Remove(collectedItem);
        
    }
}