using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brute : Enemy
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Enemy parentPolymorphFile;

    // Start is called before the first frame update
    void Awake()
    {
        this.rb = transform.GetChild(0).GetComponent<Rigidbody>();
        followThis = GameObject.Find("Core");
        enemySpeed = 0.09f;
        attackRange = 16f;

        hp = rb.transform.parent.GetComponent<Health>();
        parentPolymorphFile = rb.transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is dead, player is dead :^)
        //if (hp.IsDead()) hp.Die();

        // Execute attack if player is near
        CloseToPlayer();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    // Overrides normal attack - Ranged
    new public void AttackPlayer()
    {
        // despite direction - add speed. if rest are zero it will return 0 if multiplied by zero
        Vector3 force = parentPolymorphFile.targetPostition;

        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }
}
