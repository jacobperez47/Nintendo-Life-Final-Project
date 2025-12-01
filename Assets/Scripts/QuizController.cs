using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public MonoBehaviour PlayerMovement;
    [Header("Player Detection")] public GameObject interactPopup;

    private QuizActivator currentQuiz;
    private int correctAnswerIndex = 0;
    private int selectedAnswerIndex = 0;
    private bool quizActive = false;

    private int correctAnswerCount = 0;

    // Update is called once per frame
    void Update()
    {
        if (!quizActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hideQuiz();
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            changeSelection(-1);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            changeSelection(1);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedAnswerIndex == correctAnswerIndex)
            {
                correctAnswerCount++;
                currentQuiz.markCorrect();
                Debug.Log("Correct Total:" + correctAnswerCount);
            }
            else
            {
                Debug.Log("Wrong");
            }

            hideQuiz();
        }
    }

    public void showQuiz(string question, string[] options, int correct)
    {
        if (currentQuiz.answeredCorrectly)
        {
            Debug.Log("Quiz has already been answered correctly.");
            return;
        }

        quizActive = true;
        gameObject.SetActive(true);


        if (PlayerMovement != null)
        {
            PlayerMovement.enabled = false;
        }

        correctAnswerIndex = correct;

        questionText.color = Color.black;
        questionText.text = question;

        for (int i = 0; i < options.Length; i++)
        {
            answerTexts[i].text = options[i];
        }

        selectedAnswerIndex = 0;
        updateAnswerUI();
    }

    public void showPopup()
    {
        interactPopup.SetActive(true);
    }

    public void hideQuiz()
    {
        quizActive = false;
        gameObject.SetActive(false);

        if (PlayerMovement != null)
        {
            PlayerMovement.enabled = true;
        }
    }

    public void hidePopup()
    {
        if (interactPopup != null)
        {
            interactPopup.SetActive(false);
        }
    }

    public void changeSelection(int direction)
    {
        selectedAnswerIndex = Mathf.Clamp(selectedAnswerIndex + direction, 0, answerTexts.Length - 1);
        updateAnswerUI();
    }

    public void updateAnswerUI()
    {
        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i == selectedAnswerIndex)
            {
                answerTexts[i].color = Color.green;
            }
            else
            {
                answerTexts[i].color = Color.black;
            }
        }
    }

    public void SetQuizActivator(QuizActivator activator)
    {
        currentQuiz = activator;
    }
    public int getAnswerCount()
    {
        return correctAnswerCount;
    }
}