using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizActivator : MonoBehaviour
{
    [Header("Quiz Data Per Object")]
    [TextArea] public string question;
    public string[] options = new string[4];
    public int correctAnswerIndex = 0;
    [HideInInspector] public bool answeredCorrectly = false;
    

    private bool playerInRange = false;
    public QuizController quizController;

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateQuiz();
            quizController.hidePopup();
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
            if (!answeredCorrectly)
            {
                quizController.showPopup();
            }
        }
    }

    public void markCorrect()
    {
        answeredCorrectly = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player out of range");
            playerInRange = false;
            quizController.hidePopup();
        }
    }
    
    public void ActivateQuiz()
    {
        quizController.SetQuizActivator(this); 
    
        quizController.showQuiz(question, options, correctAnswerIndex); 
    }
}
