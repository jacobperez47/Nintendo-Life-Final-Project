using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToCollectionRoom()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Collection Room");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        // In the Editor Application.Quit() does nothing — stop play mode there for convenience
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #endif
    }
}
