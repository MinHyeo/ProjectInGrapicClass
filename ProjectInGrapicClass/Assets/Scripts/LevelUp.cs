using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour, IEatAble
{
    public void Eat() {
        GameManager.instance.level++;
    }
}
