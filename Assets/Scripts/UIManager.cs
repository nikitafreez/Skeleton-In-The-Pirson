using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    public float fadeSpeed = 1f;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    public Text coinText;

    public GameObject pauseScreen, settingScreen, levelScreen;
    public Slider musicVolSlider, sfxVolSlider;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }

        }
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void ContinueClick()
    {
        GameManager.instance.PauseUnpause();
    }

    public void OptionClick()
    {
        settingScreen.SetActive(true);
    }

    public void LevelClick()
    {
        levelScreen.SetActive(true);
    }

    public void ExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void BackClick()
    {
        settingScreen.SetActive(false);
        levelScreen.SetActive(false);

    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }

    public void PrisonLvlClick()
    {
        SceneManager.LoadScene("Skeleton in Prison");
    }
    
    public void MazeLvlClick()
    {
        SceneManager.LoadScene("DanganMaze");
    }
}
