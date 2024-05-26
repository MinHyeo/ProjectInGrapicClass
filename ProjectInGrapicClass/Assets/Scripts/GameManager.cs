using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform[] spawners;
    public float gameTime;
    public int level;
    public int score;

    private void Start()
    {
        gameTime = 0;
        level = 0;
        score = 0;

        Invoke("CoinSpawn", 0.1f);
    }
    private void FixedUpdate()
    {
        //게임 시간 측정
        gameTime += Time.fixedDeltaTime;
    }
    public void GameOver()
    {
        //Game Over
        Debug.Log("Game Over");
    }
    public void CoinSpawn()
    {
        if()
        int rand = Random.Range(0, spawners.Length);
        ObjectPooling.instance.Get(0).transform.position = spawners[rand].position;
    }
}
