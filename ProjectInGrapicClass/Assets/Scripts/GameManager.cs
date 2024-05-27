using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Ghost ghost;
    public GameObject gameOverUI;

    public Transform[] spawners;
    public float gameTime;
    public int level;
    public int score;
    public int[] nextLevelScore = { 100, 300, 700, 1500, 2500, 4000 };

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
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }
    public void GameRetry() {
        SceneManager.LoadScene(0);
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void EatItem() {
        if (score >= nextLevelScore[level]) {
            if (level + 1 >= nextLevelScore.Length)
                CoinSpawn();
            else
                LevelUpSpawn();
        }
        else
            CoinSpawn();
    }
    void CoinSpawn()
    {
        int rand = Random.Range(0, spawners.Length);
        ObjectPooling.instance.Get(0).transform.position = spawners[rand].position;
    }
    void LevelUpSpawn() {
        int rand = Random.Range(0, spawners.Length);
        ObjectPooling.instance.Get(1).transform.position = spawners[rand].position;
    }
}
