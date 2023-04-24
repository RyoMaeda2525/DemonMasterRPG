using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] CharacterSheet _characterSheet;
    [SerializeField] int _spawnLevel = 1;
    [SerializeField] float _interval = 20f;

    MonsterStatus _monster;
    float _timer = 0f;
    PauseManager _pauseManager;
    bool _pause;

    private void Update()
    {
        if (!_pause)
        {
            if (_monster && !_monster.gameObject.activeSelf)
            {
                _timer += Time.deltaTime;
                if (_timer > _interval)
                {
                    Destroy(_monster);
                    Spawn();
                    _timer = 0f;
                }
            }
            else if(!_monster) { Spawn(); }
        }
    }

    void Spawn()
    {
        _monster = MonsterStatus.Create(_characterSheet, _spawnLevel);
        _monster.tag = "EnemyMonster";
        _monster.gameObject.transform.position = this.transform.position;
    }

    private void Awake()
    {
        _pauseManager = GameManager.Instance.PauseManager;
    }

    private void OnEnable()
    {
        _pauseManager.onCommandMenu += PauseCommand;
    }

    private void OnDisable()
    {
        _pauseManager.onCommandMenu -= PauseCommand;
    }

    void PauseCommand(bool onPause)
    {
        if (onPause)
        {
            _pause = true;
        }
        else
        {
            _pause = false;
        }
    }
}
