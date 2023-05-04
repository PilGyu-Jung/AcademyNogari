using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public RectTransform pauseMenu;
    
    public bool pauseDown;

    void Start()
    {
        pauseDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseDown = !pauseDown;
            PauseActive(pauseDown);
        }
    }

    void PauseActive(bool pDown)
    {
        if (pDown)
        {
            pauseMenu.anchoredPosition = new Vector3(0f, 0f);

            Time.timeScale = 0f;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        pauseDown = false;
        pauseMenu.anchoredPosition = new Vector3(0f, 1300f);

        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ExitGame()
    {
        Application.Quit();

    }
}
