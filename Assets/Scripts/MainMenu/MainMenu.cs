using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneProvider _sceneProvider;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void StartMission()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Map"));
    }
}
