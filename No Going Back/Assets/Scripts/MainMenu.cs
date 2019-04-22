using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Title Screen
    public Text title1, title2, title3;
    public Image backGround;
    public Image backGroundGlow;
    public Text instruction;
    bool fadeIn1, fadeIn2, fadeIn3, fadeInBG, fadeInOutInstruction;
    float fadeInRate;
    float fadeOutRate;
    float fadeInOutRate;
    //General
    Stack<GameObject> screens;
    public GameObject titleScreen, mainMenu, creditsScreen, optionsScreen;
    public Image fadeImage;
    bool credits, options;
    bool main;
    bool fadeIn;
    bool back;
    bool bgGlow;
    bool play;
    public GameObject shot1, shot2, shot3;

	void Start ()
    {
        fadeInRate = 0.01f;
        fadeOutRate = 0.02f;
        fadeInOutRate = 0.01f;
        fadeIn1 = true;
        screens = new Stack<GameObject>();
        screens.Push(titleScreen);
	}
	
	void Update ()
    {
        UpdateScreens();
        if (screens.Peek() == titleScreen)
        {
            TitleScreenFade();
            if(Input.GetKeyDown(KeyCode.Return) && !fadeIn)
            {
                main = true;
            }
        }
	}

    void UpdateScreens()
    {
        if (bgGlow)
        {
            backGroundGlow.color += new Color(0, 0, 0, fadeInOutRate);
            if (backGroundGlow.color.a <= 0)
            {
                fadeInOutRate = 0.01f;
            }
            else if (backGroundGlow.color.a >= 1)
            {
                fadeInOutRate = -0.01f;
            }
        }
        if(play)
        {
            fadeImage.color += new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a >= 1f && screens.Peek()==mainMenu)
            {
                play = false;
                fadeIn = true;
                screens.Push(shot1);
                screens.Peek().SetActive(true);
                Invoke("NextShot", 3);
            }
            else if (fadeImage.color.a >= 1f && screens.Peek() == shot1)
            {
                play = false;
                fadeIn = true;
                screens.Push(shot2);
                screens.Peek().SetActive(true);
                Invoke("NextShot", 3);
            }
            else if (fadeImage.color.a >= 1f && screens.Peek() == shot2)
            {
                play = false;
                fadeIn = true;
                screens.Push(shot3);
                screens.Peek().SetActive(true);
                Invoke("NextShot", 3);
            }
            else if (fadeImage.color.a >= 1f && screens.Peek() == shot3)
            {
                play = false;
                fadeIn = true;
                Invoke("StartGame", 3);
            }
        }
        if (credits)
        {
            fadeImage.color += new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a >= 1f)
            {
                credits = false;
                fadeIn = true;
                screens.Push(creditsScreen);
                screens.Peek().SetActive(true);
            }
        }
        if (options)
        {
            fadeImage.color += new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a >= 1f)
            {
                options = false;
                fadeIn = true;
                screens.Push(optionsScreen);
                screens.Peek().SetActive(true);
            }
        }
        if (main)
        {
            fadeImage.color += new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a >= 1f)
            {
                main = false;
                fadeIn = true;
                screens.Push(mainMenu);
                screens.Peek().SetActive(true);
            }
        }
        if (back)
        {
            fadeImage.color += new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a >= 1f)
            {
                back = false;
                fadeIn = true;
                screens.Pop().SetActive(false);
            }
        }
        if (fadeIn)
        {
            fadeImage.color -= new Color(0, 0, 0, fadeOutRate);
            if (fadeImage.color.a <= 0f)
            {
                fadeIn = false;
            }
        }
    }

    void NextShot()
    {
        play = true;
    }

    //Fade Effects for Title Screen
    void TitleScreenFade()
    {
        if (fadeIn1)
        {
            title1.color += new Color(0, 0, 0, fadeInRate);
            if (title1.color.a >= 1)
            {
                fadeIn1 = false;
                fadeIn2 = true;
            }
        }
        if (fadeIn2)
        {
            title2.color += new Color(0, 0, 0, fadeInRate);
            if (title2.color.a >= 1)
            {
                fadeIn2 = false;
                fadeIn3 = true;
            }
        }
        if (fadeIn3)
        {
            title3.color += new Color(0, 0, 0, fadeInRate);
            if (title3.color.a >= 1)
            {
                fadeIn3 = false;
                fadeInBG = true;
                fadeInOutInstruction = true;
                bgGlow = true;
            }
        }
        if (fadeInBG)
        {
            backGround.color += new Color(0, 0, 0, fadeInRate);
            if (backGround.color.a >= 0.8f)
            {
                fadeInBG = false;
            }
        }
        if (fadeInOutInstruction)
        {
            instruction.color += new Color(0, 0, 0, fadeInOutRate);
            if (instruction.color.a <= 0)
            {
                fadeInOutRate = 0.01f;
            }
            else if (instruction.color.a >= 1)
            {
                fadeInOutRate = -0.01f;
            }
        }
    }

    //Play Game
    public void Play()
    {
        play = true;
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //Show Credits
    public void Credits()
    {
        if (!fadeIn)
        {
            credits = true;
        }
    }

    public void Options()
    {
        if (!fadeIn)
        {
            options = true;
        }
    }

    public void Back()
    {
        if (!fadeIn)
        {
            back = true;
        }
    }

    //Quit Application
    public void Quit()
    {
        Application.Quit();
    }
}
