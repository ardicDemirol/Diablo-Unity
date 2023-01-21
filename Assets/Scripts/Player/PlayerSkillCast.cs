using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("CoolDown Icons")]
    public Image[] CoolDownIcon;
    [Header("Out of Mana Icons")]
    public Image[] OutOfManaIcons;
    [Header("Cooldown Times")]
    public float[] coolDownTime;
    // Mana Deðerleri
    private bool faded;
    
    private int[] fadeImages = new int[] {0,0,0,0,0,0,};
    public List<float> CooldownTimesList = new List<float>();

    private Animator anim;

    private bool canAttack = true;

    private PlayerOnClick playerOnClick;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
        playerOnClick = GetComponent<PlayerOnClick>();
    }

    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            CooldownTimesList.Add(coolDownTime[i]);
        }
        
    }

    void Update()
    {
        if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))  //anim.IsInTransition ile animasyomlar arasý bir geçiþ durumunda olup olmadýðýmýzý kontrol ederiz
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
        CheckToFade();
        CheckInput();
    }

    void CheckToFade()
    {
        for (int i = 0; i < CoolDownIcon.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(CoolDownIcon[i], CooldownTimesList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }
    }

    void CheckInput()
    {
        if (anim.GetInteger("Attack") == 0)
        {
            playerOnClick.FinishedMovement = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerOnClick.FinishedMovement = true;
            }
        }

        /// Skill Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerOnClick.targetPosition = transform.position;
            if(playerOnClick.FinishedMovement && fadeImages[0] != 1 && canAttack)
            {
                fadeImages[0] = 1;
                anim.SetInteger("Attack",1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerOnClick.targetPosition = transform.position;
            if(playerOnClick.FinishedMovement && fadeImages[1] != 1 && canAttack)
            {
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[2] != 1 && canAttack)
            {
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[3] != 1 && canAttack)
            {
                fadeImages[3] = 1;
                anim.SetInteger("Attack", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[4] != 1 && canAttack)
            {
                fadeImages[4] = 1;
                anim.SetInteger("Attack", 5);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[5] != 1 && canAttack)
            {
                fadeImages[5] = 1;
                anim.SetInteger("Attack", 6);
            }
        }
        else
        {
            anim.SetInteger("Attack",0);

        }
    }
 
    bool FadeAndWait(Image fadeImage,float fadeTime)
    {
        faded = false;
        if(fadeImage == null)
        {
            return faded;
        }
        if (!fadeImage.gameObject.activeInHierarchy) 
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if(fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }
}
