using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    public Sprite musicOn, musicOf;

    public void Start()
    {
        if (PlayerPrefs.GetString("music") == "No" && gameObject.name == "Music" && GetComponent<AudioSource>() != null)
        {
            GetComponent<Image>().sprite = musicOf;
        }
        else if (PlayerPrefs.GetString("music") != "No" && GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (PlayerPrefs.GetString("music") != "No" && GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void MusicWork()
    {
        //сейчас нужно выключить музыку
        if (PlayerPrefs.GetString("music") == "No")
        {
            PlayerPrefs.SetString("music", "Yes");
            GetComponent<Image>().sprite = musicOn;
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = musicOf;
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}