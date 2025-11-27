using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignController : MonoBehaviour
{
    [Header("Sign Text")]
    [TextArea(2,5)]
    public string signText;

    [Header("UI Elements")]
    public GameObject signPanel;
    public TMP_Text signTextUI;

    [Header("Player Detection")]
    public GameObject interactPopup;

    private bool playerInRange = false;
    private bool signActive = false;
    public MonoBehaviour playerMovement;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            showSign();
        }

        if(signActive && Input.GetKeyDown(KeyCode.Q))
        {
            hideSign();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
            showPopup();
        }
    } 

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player out of range");
            playerInRange = false;
            hidePopup();
        }
    }

    void showPopup()
    {
        interactPopup.SetActive(true);
    }

    void hidePopup()
    {
        if(interactPopup != null)
        {
            interactPopup.SetActive(false);
        }
    }

    void showSign()
    {
        signActive = true;
        signPanel.SetActive(true);
        signTextUI.text = signText;
        signTextUI.color = Color.black;

        hidePopup();
        Time.timeScale = 0f;
        playerMovement.enabled = false;
    }

    void hideSign()
    {
        signActive = false;
        signPanel.SetActive(false);

        Time.timeScale = 1f;
        playerMovement.enabled = true;
    }
}
