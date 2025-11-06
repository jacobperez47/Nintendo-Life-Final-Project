using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    
    private Vector2 movement;
    
    public CollectableManager collectableManager;
    // Start is called before the first frame update
    void Start()
    {
     rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movement * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger");
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectableManager.count++;
        }
    }
}
