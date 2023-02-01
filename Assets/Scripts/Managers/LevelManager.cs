using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int currentExp;
    private int level;
    private int expToNextLevel;

    public int GetLevel { get { return level + 1; } }

    public Image expBar;
    public Text levelText;

    public GameObject levelUpVFX;
    private Transform player;

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
        expBar.fillAmount = 0;
        UpdateLevelText();
        player = GameObject.Find("Player").gameObject.transform;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        expBar.fillAmount = (float)currentExp/expToNextLevel;
        if (currentExp > expToNextLevel)
        {
            level++;
            GameObject levelUpVFXClone = Instantiate(levelUpVFX,player.position,Quaternion.identity);
            levelUpVFXClone.transform.SetParent(player);
            UpdateLevelText();
            currentExp -= expToNextLevel;
            expBar.fillAmount = 0f;
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

    void UpdateLevelText()
    {
        levelText.text = GetLevel.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            AddExp(20);
            Debug.Log(level);

        }
    }
}
