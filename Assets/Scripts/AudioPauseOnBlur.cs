using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioPauseOnBlur : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Event.current.Use(); // �������� ����������� ����
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        // ����� ����� �������� (��������, �������� �������)
        if (!hasFocus)
        {
            PauseAllAudio();
            PlayerPrefs.SetString("music", "No");
        }
        else
        {
            ResumeAllAudio();
            PlayerPrefs.SetString("music", "Yes");
        }
    }

    private void PauseAllAudio()
    {
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Stop();
        }
    }

    private void ResumeAllAudio()
    {
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Play();
        }
    }
}