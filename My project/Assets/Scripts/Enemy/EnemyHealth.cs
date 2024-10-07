using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float health = 3;
    Image healthBar;
    float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar = GetComponentsInChildren<Image>()[1] ;
        healthBar.fillAmount = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
