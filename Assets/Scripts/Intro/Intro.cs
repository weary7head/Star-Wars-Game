using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class Intro : MonoBehaviour, IPointerClickHandler
{
    private SceneProvider _sceneProvider;
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
        _videoPlayer = GetComponent<VideoPlayer>();
        Cursor.visible = false;
    }
    
    private void OnEnable()
    {
        _videoPlayer.loopPointReached += EndReached;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= EndReached;
    }

    private void EndReached(VideoPlayer source)
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Menu"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Menu"));
    }
}
