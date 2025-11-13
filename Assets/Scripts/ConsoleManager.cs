using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    private bool alreadyCollected = false;

    private SpriteRenderer spriteRenderer;

    public Sprite collectedSprite;

    public int id;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        if (alreadyCollected)
        {
            spriteRenderer.color = new Color(0, 0.9064064f, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!alreadyCollected)
            {
                alreadyCollected = true;
            }

            print("console found");
            CollectionTracker.add((id));
            gameObject.SetActive(false);
        }
    }
}