using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    bool isPlay;
    float musicVolume = .5f;
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
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
}
