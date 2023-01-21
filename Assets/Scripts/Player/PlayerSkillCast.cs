using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("Mana Settings")]
    public float totalMana = 100f;
    public float manaRegenSpeed = 2f;
    public Image manaBar;
    [Header("CoolDown Icons")]
    public Image[] coolDownIcon;
    [Header("Out of Mana Icons")]
    public Image[] outOfManaIcons;
    [Header("Cooldown Times")]
    public float[] coolDownTime;
    [Header("Mana Amounts")]
    public float[] skillManaAmounts;

    private bool faded;
    
    private int[] fadeImages = new int[] {0,0,0,0,0,0,};
    [HideInInspector] public List<float> CooldownTimesList = new List<float>();
    private List<float> manaAmountList = new List<float>();

    private Animator anim;

    private bool canAttack = true;

    private PlayerOnClick playerOnClick;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
        playerOnClick = GetComponent<PlayerOnClick>();
        manaBar = GameObject.Find("ManaOrb").GetComponent<Image>();
    }

    void Start()
    {
        AddList();
    }

    void AddList()
    {
        for (int i = 0; i < 6; i++)
        {
            CooldownTimesList.Add(coolDownTime[i]);
        }
        for (int i = 0; i < 6; i++)
        {
            manaAmountList.Add(skillManaAmounts[i]);
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

        if (anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TurnThePlayer();
        }

        if(totalMana < 100f)
        {
            totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana/100f;
        }
        
        CheckMana();
        CheckToFade();
        CheckInput();
    }

    void CheckToFade()
    {
        for (int i = 0; i < coolDownIcon.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(coolDownIcon[i], CooldownTimesList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }
    }

    void CheckMana()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (totalMana < manaAmountList[i])
            {
                outOfManaIcons[i].gameObject.SetActive(true);
            }
            else
            {
                outOfManaIcons[i].gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && totalMana >= skillManaAmounts[0])
        {
            playerOnClick.targetPosition = transform.position;
            if(playerOnClick.FinishedMovement && fadeImages[0] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[0];
                fadeImages[0] = 1;
                anim.SetInteger("Attack",1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && totalMana >= skillManaAmounts[1])
        {
            playerOnClick.targetPosition = transform.position;
            if(playerOnClick.FinishedMovement && fadeImages[1] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[1];
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && totalMana >= skillManaAmounts[2])
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[2] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[2];
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && totalMana >= skillManaAmounts[3])
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[3] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[3];
                fadeImages[3] = 1;
                anim.SetInteger("Attack", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && totalMana >= skillManaAmounts[4])
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[4] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[4];
                fadeImages[4] = 1;
                anim.SetInteger("Attack", 5);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && totalMana >= skillManaAmounts[5])
        {
            playerOnClick.targetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[5] != 1 && canAttack)
            {
                totalMana -= skillManaAmounts[5];
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

    void TurnThePlayer()
    {
        Vector3 targetPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position),
            playerOnClick.turnSpeed * Time.deltaTime);
    }
}
