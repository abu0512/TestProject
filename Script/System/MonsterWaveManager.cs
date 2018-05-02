using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveManager : MonoBehaviour
{
    private List<MonsterWave> _waves = new List<MonsterWave>();
    private int _curWave;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MonsterWave wave = transform.GetChild(i).GetComponent<MonsterWave>();
            wave.gameObject.SetActive(false);
            _waves.Add(wave);
        }

        _curWave = 0;
        StartWave();
    }

    void Update()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (!_waves[_curWave].gameObject.activeInHierarchy)
            return;

        if (_waves[_curWave].RunCheck())
            return;

        _waves[_curWave].gameObject.SetActive(false);

        if (_curWave >= _waves.Count - 1)
            return;

        _curWave++;

        _waves[_curWave].gameObject.SetActive(true);
        _waves[_curWave].InitWave();
        Stage1.I.InitMonsters();
    }

    public void StartWave()
    {
        if (_curWave != 0)
            return;

        _waves[_curWave].gameObject.SetActive(true);
        _waves[_curWave].InitWave();
        Stage1.I.InitMonsters();

    }
}
