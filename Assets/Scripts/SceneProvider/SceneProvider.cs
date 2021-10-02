using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProvider
{
    public IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation asyncLoad =  SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }
    }

    public void SetActiveScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
    }
}
