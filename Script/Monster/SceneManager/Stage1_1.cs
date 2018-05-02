using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_1 : MonoBehaviour
{
    private QueenMushroom _queen;
    private GuardMushroom _guard;

    public GameObject GuardM;
    public GameObject QueenM;

    List<GameObject> GuardMushroom = new List<GameObject>();
    List<GameObject> GuardPosition = new List<GameObject>();

    private float sum = 0;

    private void Awake()
    {
        _queen = GameObject.FindGameObjectWithTag("Queen").GetComponent<QueenMushroom>();
        _guard = GameObject.FindGameObjectWithTag("Guard").GetComponent<GuardMushroom>();

        //GuardMushroomInit();
    }

    /*
    void GuardMushroomInit()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject _obj = Instantiate(GuardM) as GameObject;
            GuardMushroom.Add(_obj);
            for (int j = i; j < i+1; j++)
            {               
                GuardMushroom[j].SetActive(true);
                GuardMushroom[j].transform.position = new Vector3(-12f + sum, 0, 20f);
                sum += 3f;
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

    void Update()
    {
        if (_queen.isDead)
        {
            Debug.Log("gggggggggggggggggㅎㅎㅎㅎㅎ");
        }
    }
}
