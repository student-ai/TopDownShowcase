using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float shootSpeed = 10f;
    [SerializeField]
    float bulletLifetime = 2.0f;
    float timer = 0;
    [SerializeField]
    float shootDelay = 0.5f;
    [SerializeField]
    float shootDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 shootDir = player.transform.position - transform.position;
        if(timer > shootDelay && shootDir.magnitude < shootDistance)
        {
            timer = 0;
            shootDir.Normalize();
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}
