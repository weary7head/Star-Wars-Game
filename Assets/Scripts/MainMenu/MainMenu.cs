using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SceneProvider _sceneProvider;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowIntro()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Intro"));
    }

    public void StartLevel()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("TrainingLevel"));
    }
}
