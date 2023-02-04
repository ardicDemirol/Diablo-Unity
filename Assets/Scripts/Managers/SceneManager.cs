using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    BossState bossState;
    public GameObject bossHealthBar;
    void Awake()
    {
        bossState = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossState>();
        AudioManager.Instance.PlayGameMusic();
    }

    void Update()
    {
        if (bossState.state == BossState.State.SLEEP || bossState.state == BossState.State.DEATH)
        {
            bossHealthBar.SetActive(false);
        }
        else
        {
            bossHealthBar.SetActive(true);
        }
        
    }

}
