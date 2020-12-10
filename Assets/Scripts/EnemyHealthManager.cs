using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    public int deathSound;

    public static EnemyHealthManager instance;

    private void Awake(){
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(){
        currentHealth--;
        if(currentHealth <= 0){
            int rnd = Random.Range(7, 9);
            AudioManager.instance.PlaySFX(rnd);
            PlayerController.instance.Bounce();
        }
    }

    public void DestroyEnemy(){
        Destroy(this.gameObject);
        int rnd = Random.Range(0, 2);
        Instantiate(GameManager.instance.itemList[rnd], EnemyController.instance.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
    }
}
