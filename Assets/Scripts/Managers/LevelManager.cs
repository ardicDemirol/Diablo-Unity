using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int currentExp;
    private int level;
    private int expToNextLevel;

    public int GetLevel { get { return level; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        level = 0;
        currentExp= 0;
        expToNextLevel = 100;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        if (currentExp > expToNextLevel)
        {
            level++;
            currentExp -= expToNextLevel;
        }
    }

    private void OnEnable()
    {
        EnemyHealth.onDeath += AddExp;
    }
    private void OnDisable()
    {
        EnemyHealth.onDeath -= AddExp;
    }

    private void Update()
    {
        Debug.Log(currentExp);
    }
}
