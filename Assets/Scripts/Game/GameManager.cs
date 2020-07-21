using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject pauseMenuCanvas;

    bool isActive;
    bool onCursor;
    bool isPlay;
    float musicVolume = .5f;

    MusicPlayer musicPlayer;
    AudioSource audioSource;

    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        audioSource = musicPlayer.GetComponent<AudioSource>();

        Cursor.visible = true;
    }
    private void Update()
    {
        audioSource.volume = musicVolume;

        ActiveCursor();
        PauseMenu();
    }
    private void ActiveCursor()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            onCursor = !onCursor;

            if (onCursor)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
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
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void LoadMainMenuScene()
    {
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
                hudCanvas.SetActive(false);
                pauseMenuCanvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                hudCanvas.SetActive(true);
                pauseMenuCanvas.SetActive(false);
            }
        }
    }
    public void BackToGameClick()
    {
        Time.timeScale = 1;
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }
    public void MusicHandler(float vol)
    {
        musicVolume = vol;
    }
    public void MusicOnOff()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}
