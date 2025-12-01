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
    [Header("Dashing")] [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    private Vector2 _dashDir;
    private float dashTimer;
    private bool canDash = true;
    private bool isDashing;
    private AudioSource collectionSound;
    private PlayerInput input;
    private Animator animator;

    public CollectableManager collectableManager;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        input.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = _dashDir.normalized * dashSpeed;

            return;
        }

        rb.velocity = movement * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking",true);
        if (!isDashing)
        {

            if (context.canceled)
            {
                animator.SetBool("isWalking",false);
                animator.SetFloat("LastInputX", movement.x);
                animator.SetFloat("LastInputY", movement.y);
            }
            
            movement = context.ReadValue<Vector2>();
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash && context.performed)
        {
            print("Dash");

            isDashing = true;
            canDash = false;
            _dashDir = movement;
            Physics2D.IgnoreLayerCollision(3, 6, true);
            if (_dashDir == Vector2.zero)
            {
                _dashDir = new Vector2(animator.GetFloat("LastInputX"), animator.GetFloat("LastInputY"));
            }

            StartCoroutine(StopDashing());
            StartCoroutine(DashCooldown());
        }
    }

    public void ResetMovement()
    {
        movement = Vector2.zero;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        StopAllCoroutines();
        

        isDashing = false;
        canDash = true;



        Physics2D.IgnoreLayerCollision(3, 6, false);


      if (input != null) 
          {
              input.enabled = true;
          }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<Accessory_Script>().isCollected = true;
            collectionSound = other.GetComponent<AudioSource>();
            AudioClip clip = collectionSound.clip;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            CollectionTracker.addAccessory(other.GetComponent<Accessory_Script>().id);
            other.GetComponent<Accessory_Script>().Collect();
            other.gameObject.SetActive(false);
            
        }

        if (other.CompareTag("Console"))
        {
            collectionSound = other.GetComponent<AudioSource>();
            AudioClip clip = collectionSound.clip;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            print("console found");
            ConsoleManager instance = other.GetComponent<ConsoleManager>();
            CollectionTracker.add(instance.id);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        print("Stop Dashing");
        input.enabled = false;
        Physics2D.IgnoreLayerCollision(3, 6, false);



        isDashing = false;
        yield return new WaitForSeconds(0.1f);
        ResetMovement();
        input.enabled = true;
    }


    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}