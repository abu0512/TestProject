using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    private static Stage1 _instance;
    public static Stage1 I { get { return _instance; } }

    public BulletObjectPool BulletPool;

    private List<GuardMushroom> _guards = new List<GuardMushroom>();
    private List<QueenMushroom> _queens = new List<QueenMushroom>();
    private bool _changeMode;

    public bool ChangeMode { get { return _changeMode; } set { _changeMode = value; } }

    public int GuardCount
    {
        get
        {
            int cnt = 0;

            foreach (GuardMushroom guard in _guards)
            {
                if (guard.gameObject.activeInHierarchy)
                    cnt++;
            }

            return cnt;
        }
    }
    public int QueenCount
    {
        get
        {
            int cnt = 0;

            foreach (QueenMushroom queen in _queens)
            {
                if (queen.gameObject.activeInHierarchy)
                    cnt++;
            }

            return cnt;
        }
    }

    private void Awake()
    {
        //_queen = GameObject.FindGameObjectWithTag("Queen").GetComponent<QueenMushroom>();

        //GameObject[] mushroom = GameObject.FindGameObjectsWithTag("Guard");
        //_guards = new GuardMushroom[mushroom.Length];

        //for (int i = 0; i < mushroom.Length; i++)
        //{
        //    _guards[i] = mushroom[i].GetComponent<GuardMushroom>();
        //}

        //for (int j = 0; j < _guards.Length; j++)
        //{
        //    if (_GuardMushroom[j].activeSelf)
        //    {
        //        ProbCount++;
        //    }
        //}
        _instance = this;

        foreach (GuardMushroom guard in FindObjectsOfType<GuardMushroom>())
        {
            _guards.Add(guard);
        }

        foreach (QueenMushroom queen in FindObjectsOfType<QueenMushroom>())
        {
            _queens.Add(queen);
        }
    }

    /*void GuardMushroomisAllDead()
    {
        if (GuardCount > 0)
            return;

        foreach (QueenMushroom queen in _queens)
        {
            queen.HealTime = false;
            queen.HealStart = false;
        }
    }*/

    void BulletPoolSet()
    {
        //if(QueenCount <= 0)
        //    BulletPool.SetActive(false);
        //else
        //{
        //    BulletPool.SetActive(true);
        //}
    }

    void HealTimeCheck()
    {
        if (GuardCount == 0 && QueenCount == 0)
            return;

        foreach (GuardMushroom guard in _guards)
        {
            if (guard.Stat.Hp <= 100.0f)
            {
                foreach (QueenMushroom queen in _queens)
                {
                    queen.HealTime = true;
                }
            }
        }
        
        foreach (QueenMushroom queen in _queens)
        {
            if (queen.Stat.Hp <= 100.0f)
            {
                foreach (QueenMushroom queens in _queens)
                {
                    queens.HealTime = true;
                }
            }
        }
    }

    void HealTimeStart()
    {
        foreach (QueenMushroom queen in _queens)
        {
            if (!queen.HealStart)
                continue;

               queen.SetMonsterHeal(queen.HealPoint);
            foreach (GuardMushroom guard in _guards)
            {
                guard.SetMonsterHeal(queen.HealPoint);
            }

            queen.HealStart = false;
            queen.HealTime = false;
        }

    }

    void GuardModeChange()
    {
        if (QueenCount > 0)
            return;

        if (_changeMode)
            return;

        int cnt = 0;

        foreach (GuardMushroom guard in _guards)
        {
            guard.SBombing = true;
            cnt++;

            if (cnt >= GuardCount / 2)
                break;
        }

        foreach (GuardMushroom guard in _guards)
        {
            if (guard.SBombing)
                continue;

            guard.QueenisAllDead = true;
        }

        _changeMode = true;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {

    //    }
    //}

    void Update()
    {
        BulletPoolSet();
        HealTimeCheck();
        HealTimeStart();
        GuardModeChange();
    }

    public void InitMonsters()
    {
        _guards.Clear();
        _queens.Clear();
        foreach (GuardMushroom guard in FindObjectsOfType<GuardMushroom>())
        {
            _guards.Add(guard);
        }

        foreach (QueenMushroom queen in FindObjectsOfType<QueenMushroom>())
        {
            _queens.Add(queen);
        }
    }
}
