using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
        Time.timeScale = 0;
    }

    [Header("Game UI")]
    public GameObject gameStartUI;
    public GameObject gameOverUI;
    public Text endScoreText;
    public Text bestScoreText;
    public Text scoreText;

    [Header("Game Objects")]
    public Ghost ghost;
    public Transform[] spawners;

    [Header("Game Data")]
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

        //Score Text Update
        if(PlayerPrefs.GetInt("BestScore", 0) < score)
            PlayerPrefs.SetInt("BestScore", score);

        endScoreText.text = "Score : " + score;
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore", 0);
    }
    public void GameStart() {
        gameStartUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameRetry() {
        SceneManager.LoadScene(0);
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void ScoreUpdate() {
        scoreText.text = "Score : " + score;
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
        //코인 스폰
        int rand = Random.Range(0, spawners.Length);
        ObjectPooling.instance.Get(0).transform.position = spawners[rand].position;
    }
    void LevelUpSpawn() {
        int rand = Random.Range(0, spawners.Length);
        ObjectPooling.instance.Get(1).transform.position = spawners[rand].position;
    }
}