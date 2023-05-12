using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static float Money;
    public float startMoney = 200f;
    public float goldPerSecond;

    public static float lives;
    public float startLives;

    private float accumulatedTime = 0f;

    private void Start()
    {
        Money = startMoney;
        lives = startLives;
    }

    private void Update()
    {
        accumulatedTime += Time.deltaTime;

        if(accumulatedTime >= 1f)
        {
            accumulatedTime = 0f;

            Money += goldPerSecond;
        }
        
    }

    public float GetMoney()
    {
        return Money;
    }

}
