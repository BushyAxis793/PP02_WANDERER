using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isPlay;
    float musicVolume = 1f;
    
    void Start()
    {
        isPlay = true;
        DontDestroyOnLoad(this);
        //Cursor.visible = false;
    }
    private void Update()
    {
        
        GetComponent<AudioSource>().volume = musicVolume;
    }

    public void MusicHandler(float vol)
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

        musicVolume = vol;
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


}
