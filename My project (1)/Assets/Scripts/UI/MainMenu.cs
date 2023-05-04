using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public RectTransform Screen_Start;
    public RectTransform Screen_Hero;
    public RectTransform Screen_Stage;
    public RectTransform Screen_Option;

    Vector2 origin_Screen_Hero;
    Vector2 origin_Screen_Stage;
    Vector2 origin_Screen_Option;

    bool is_Screen_Hero;
    bool is_Screen_Stage;
    bool is_Screen_Option;
    bool chooseHero;


    private void Awake()
    {
        origin_Screen_Hero = Screen_Hero.anchoredPosition;
        origin_Screen_Stage = Screen_Stage.anchoredPosition;
        origin_Screen_Option = Screen_Option.anchoredPosition;

    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        is_Screen_Hero = true;
        Screen_Hero.anchoredPosition = new Vector2(0f, 0f);
    }

    public void PressOption()
    {
        is_Screen_Option = true;
        Screen_Option.anchoredPosition = new Vector2(0f, 0f);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("GameStart");
    }
}
