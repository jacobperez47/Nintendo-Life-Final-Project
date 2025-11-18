using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite collectedSprite;

    private AudioSource collectionSound;

    public int id;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (CollectionTracker.get(1))
        {
            spriteRenderer.color = new Color(0, 0.9064064f, 1);
        }

        collectionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
    }

    public void playSound()
    {
        collectionSound.Play();
    }
    
}   