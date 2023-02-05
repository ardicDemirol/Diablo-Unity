using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    BossState bossState;
    public GameObject bossHealthBar;
    void Start()
    {
        bossState = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossState>();
        AudioManager.Instance.PlayGameMusic();
    }

    void Update()
    {
        if (bossState.state == BossState.State.SLEEP || bossState.state == BossState.State.DEATH)
        {
            if(bossHealthBar != null)
            {
                bossHealthBar.SetActive(false);
            }
            
        }
        else
        {
            if(bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
            }
            
        }
        if (Boss.bossDeath)
        {
            Invoke(nameof(RestartScene), 4f);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            Invoke(nameof(RestartScene), 4f);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
