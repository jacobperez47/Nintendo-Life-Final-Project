using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizCounter : MonoBehaviour
{
    
    public static QuizCounter Instance;
    
    public TMP_Text quizCounter;
    public int quizCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        quizCounter.text = quizCount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddQuiz()
    {
        quizCount++;
        quizCounter.text += quizCount.ToString();
    }
}
