using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory_Script : MonoBehaviour
{
    public int id;

    public bool isCollected = false;
    
    private AudioSource collectionSound;
    
    public CollectableManager manager; 
    

    void Start()
    {
            
        manager = FindObjectOfType<CollectableManager>();
        if (CollectionTracker.getAccessory(id))
        {
            isCollected = true;
        }

        if (isCollected)
        {
            if (manager != null)
            {
                manager.RemoveCollectable(this);
            }
            gameObject.SetActive(false);
        }
    }
    


    public void Collect()
    {
        isCollected = true;
    
        if (manager != null)
        {
            manager.RemoveCollectable(this);
        }

        Destroy(gameObject); 
    }

 
}