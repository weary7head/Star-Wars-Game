using System;
using UnityEngine;

public class QuestLevel1 : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private int _winCount;
    private int _deathCount = 0;
    private SceneProvider _sceneProvider;
    private event Action QuestCompleted;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
    }

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.DeadCountChanged += OnDeathCountChanged;
        }
        QuestCompleted += QuestComplete;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.DeadCountChanged -= OnDeathCountChanged;
        }
        QuestCompleted -= QuestComplete;
    }

    private void OnDeathCountChanged()
    {
        ++_deathCount;
        if (_deathCount == _winCount)
        {
            QuestCompleted?.Invoke();
        }
    }

    private void QuestComplete()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Menu"));
    }
}
