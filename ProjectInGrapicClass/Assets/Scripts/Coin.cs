using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IEatAble
{
    private int[] scoreList = { 10, 20, 30, 50, 70, 100 };
    private int score;

    private void OnEnable()
    {
        score = scoreList[GameManager.instance.level];
    }
    public void Eat()
    {
        GameManager.instance.score += score;
    }
}
