using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, EnemyInterface
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public GameObject followThis;
    [SerializeField] public Health hp;

    [SerializeField] public float enemySpeed;
    [SerializeField] public float attackRange;
    [SerializeField] public float distanceFromPlayer;

    [SerializeField] public bool attacking = false;
    public Vector3 targetPostition;

    // Default Movement - Can be overridden for more complex movement
    public void FollowPlayer()
    {
        targetPostition = new Vector3(followThis.transform.position.x,
                                              this.rb.transform.position.y,
                                              followThis.transform.position.z);

        if (!attacking)
        {
            // Get orientation point to follow and look at ball so that we can always determine where to go
            this.rb.transform.position = Vector3.MoveTowards(this.rb.transform.position,
                                                             followThis.transform.position,
                                                             enemySpeed);
            this.rb.transform.LookAt(targetPostition);
        }
        else
        {
            // Get orientation point to follow and look at ball so that we can always determine where to go
            this.rb.transform.position = Vector3.MoveTowards(this.rb.transform.position,
                                                             followThis.transform.position,
                                                             enemySpeed * 0.05f);
            this.rb.transform.LookAt(targetPostition);
        }
    }

    public void CloseToPlayer()
    {
        distanceFromPlayer = Vector3.Distance(rb.transform.position, followThis.transform.position);

        // Player can be attacked
        if (distanceFromPlayer <= attackRange)
        {
            attacking = true;
            AttackPlayer();
            //Debug.Log("Close: " +distanceFromPlayer);
            //hp.TakeDamage(this.transform, 5f);
        }
        else // Player too far too attack
        {
            attacking = false;
            return;
        }
    }

    //Default attack
    public void AttackPlayer()
    {

    }
}
