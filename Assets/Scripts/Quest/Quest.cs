using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private Target[] _targets;
    private int _winCount = 3;
    private int _deathCount = 0;
    private SceneProvider _sceneProvider;
    private event Action OnQuestCompleted;

    private void Awake()
    {
        _sceneProvider = new SceneProvider();
    }

    private void OnEnable()
    {
        foreach (Target target in _targets)
        {
            target.OnDead += IncreaseDeathCount;
        }
        OnQuestCompleted += QuestComplete;
    }

    private void OnDisable()
    {
        foreach (Target target in _targets)
        {
            target.OnDead -= IncreaseDeathCount;
        }
        OnQuestCompleted -= QuestComplete;
    }

    private void IncreaseDeathCount()
    {
        ++_deathCount;
        if (_deathCount == _winCount)
        {
            OnQuestCompleted?.Invoke();
        }
    }

    private void QuestComplete()
    {
        StartCoroutine(_sceneProvider.LoadSceneAsync("Menu"));
    }
}
