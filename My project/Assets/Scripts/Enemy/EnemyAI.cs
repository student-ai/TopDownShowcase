using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    float chaseSpeed = 10f;
    [SerializeField]
    float chaseTriggerDistance = 5.0f;
    [SerializeField]
    bool returnHome = true;
    Vector3 home;
    float timer = 0;
    [SerializeField]
    float returnDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // If the player gets too close
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDir = playerPosition - transform.position;
        Vector3 homeDir = home - transform.position;
        if (chaseDir.magnitude < chaseTriggerDistance)
        {
            // Chase the player
            // Chase direction = players position - my current position (enemy pos')
            // Move in the direction of the player
            timer = 0;
            chaseDir.Normalize();
            GetComponent<Rigidbody2D>().velocity = chaseDir * chaseSpeed;
        }
        else if (returnHome && homeDir.magnitude > 0.2f)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            // Return home
            if (timer > returnDelay)
            {
                homeDir.Normalize();
                GetComponent<Rigidbody2D>().velocity = homeDir * chaseSpeed;

            }
        }
        else
        {
            // If the player is not close & we're not trying to return home, stop moving
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}