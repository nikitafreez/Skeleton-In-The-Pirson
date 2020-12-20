using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && BossController.instance.canDamage == true)
        {
            BossController.instance.canDamage = false;
            BossController.instance.DamageBoss();
            PlayerController.instance.Bounce();
        }
    }
}
