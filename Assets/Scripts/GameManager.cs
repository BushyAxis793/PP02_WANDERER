using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isActive;
    bool isPlay;
    float musicVolume = .5f;
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject pauseMenuCanvas;

    void Start()
    {
        isPlay = true;
        DontDestroyOnLoad(this);
        // Cursor.visible = false;
    }
    private void Update()
    {
        PauseMenu();
        GetComponent<AudioSource>().volume = musicVolume;
    }


    public void MusicHandler(float vol)
    {
        musicVolume = vol;
    }

    public void MusicOnOff()
    {
        isPlay = !isPlay;
        if (isPlay)
        {

            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetLowGraphics()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void SetMediumGraphics()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void SetHighGraphics()
    {
        QualitySettings.SetQualityLevel(3);
    }

    public void LoadGameScene()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenuScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;

            if (isActive)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                hudCanvas.SetActive(false);
                pauseMenuCanvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                hudCanvas.SetActive(true);
                pauseMenuCanvas.SetActive(false);
            }
        }
    }

    public void BackToGameClick()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }


}
