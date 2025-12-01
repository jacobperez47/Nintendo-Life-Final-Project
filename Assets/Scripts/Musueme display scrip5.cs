using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CollectionDisplayScript : MonoBehaviour
{
    public int id;

    public Sprite clearSprite;

    private SpriteRenderer spriteRenderer;

    bool obtained = false;

    public bool isConsole;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!obtained)
        {
            if (isConsole)
            {
                if (CollectionTracker.get(id))
                {
                    obtained = true;
                    spriteRenderer.sprite = clearSprite;
                }
            }
            else
            {
                if (CollectionTracker.getAccessory(id))
                {
                    obtained = true;
                    spriteRenderer.sprite = clearSprite;
                }
                
            }
        }
    }
}