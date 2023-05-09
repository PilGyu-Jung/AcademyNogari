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
    Vector2 zeroVec = new Vector2(0f, 0f);

    bool is_Screen_Hero;
    bool is_Screen_Stage;
    bool is_Screen_Option;
    bool chooseHero;



    private void Start()
    {
        origin_Screen_Hero = Screen_Hero.anchoredPosition;
        origin_Screen_Stage = Screen_Stage.anchoredPosition;
        origin_Screen_Option = Screen_Option.anchoredPosition;
        

    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        is_Screen_Hero = true;
        MovingMainMenu(is_Screen_Hero);
        Screen_Hero.anchoredPosition = zeroVec;
    }

    public void StageOn()
    {
        is_Screen_Stage = true;
        Screen_Hero.anchoredPosition = origin_Screen_Hero;
        Screen_Stage.anchoredPosition = zeroVec;
    }

    public void StageOff()
    {
        is_Screen_Stage = false;
        Screen_Hero.anchoredPosition = zeroVec;
        Screen_Stage.anchoredPosition = origin_Screen_Stage;

    }

    public void PressOption()
    {
        is_Screen_Option = true;
        MovingMainMenu(is_Screen_Option);
        Screen_Option.anchoredPosition = zeroVec;

    }

    public void QuitOption()
    {
        is_Screen_Option = false;
        MovingMainMenu(is_Screen_Option);
        Screen_Option.anchoredPosition = origin_Screen_Option;
    }

    public void QuitHero()
    {
        is_Screen_Hero = false;
        MovingMainMenu(is_Screen_Hero);
        Screen_Hero.anchoredPosition = origin_Screen_Hero;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("GameStart");
    }

    void MovingMainMenu(bool otherscreen)
    {
        if(!otherscreen)
        {
            Screen_Start.anchoredPosition = zeroVec;
        }
        else
        {
            Screen_Start.anchoredPosition = new Vector2(0, -1300f);
        }
    }

}
