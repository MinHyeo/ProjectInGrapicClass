using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject[] prefabs;
    public List<GameObject>[] objects;

    private void Start()
    {
        objects = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            objects[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject pool = null;
        for(int i = 0; i < objects[index].Count; i++)
        {
            if (!objects[index][i].activeSelf)
            {
                pool = objects[index][i];
                pool.SetActive(true);
            }
        }
        if(pool == null)
        {
            pool = Instantiate(prefabs[index], transform);
        }
            

        return pool;
    }
}
