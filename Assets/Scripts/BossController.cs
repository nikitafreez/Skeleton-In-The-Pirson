using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Animator anim;

    public GameObject victoryZone;
    public float waitToShowExit;
    public bool canDamage = true;

    public enum BossPhase { intro, phase1, phase2, phase3, end};
    public BossPhase currentPhase = BossPhase.intro;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    private void Awake()
    {
        instance = this;
    }

   void Start()
    {
    }

    void Update()
    {
        if(GameManager.instance.isRespawning)
        {
            currentPhase = BossPhase.intro;

            anim.SetBool("Phase1", false);
            anim.SetBool("Phase2", false);
            anim.SetBool("Phase3", false);


            gameObject.SetActive(false);

            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(true);

            GameManager.instance.isRespawning = false;

        }
    }

    public void DamageBoss()
    {
        AudioManager.instance.PlaySFX(8);
        currentPhase++;
        Debug.Log(currentPhase);
        if (currentPhase != BossPhase.end)
        {
            anim.SetTrigger("Hurt");
        }

        switch (currentPhase)
        {
            case BossPhase.phase1:
                anim.SetBool("Phase1", true);
                break;

            case BossPhase.phase2:
                anim.SetBool("Phase2", true);
                anim.SetBool("Phase1", false);
                break;

            case BossPhase.phase3:
                anim.SetBool("Phase3", true);
                anim.SetBool("Phase2", false);
                break;

            case BossPhase.end:
                victoryZone.SetActive(true);
                StartCoroutine(EndBoss());
                break;
        }
        canDamage = true;
    }

    IEnumerator EndBoss()
    {
        anim.SetTrigger("End");
        AudioManager.instance.PlaySFX(7);
        yield return new WaitForSeconds(waitToShowExit);
        victoryZone.SetActive(true);
    }
}
