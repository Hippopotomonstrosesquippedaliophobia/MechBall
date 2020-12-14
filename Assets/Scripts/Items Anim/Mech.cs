using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Items
{
    public bool mechIsOnPlayer = false;
    public playerController player;
    public BulletPool bullets;

    public Transform leftBarrell, rightBarrell;
    public Transform cam;

    [Range(0, 1)] // 0 is left, 1 is right
    public float alternateFire = 1;

    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.Find("mech").GetComponent<BulletPool>(); ;
        cam = GameObject.Find("Player Camera").transform;
        player = GameObject.Find("Character").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ////get the objects current position and put it in a variable so we can access it later with less code
        //Vector3 pos = thisItem.transform.position;
        ////calculate what the new Y position will be
        //float newY = Mathf.Sin(Time.time * speed) + (height * 2);
        ////set the object's Y to the new calculated Y
        //thisItem.transform.position = new Vector3(pos.x, newY, pos.z) * height;

        //player = player.GetComponent<playerController>();

        //mechIsOnPlayer = player.wearingMech;

        // if ( mechIsOnPlayer && Input.GetKey(KeyCode.Mouse0))
        //{
        //    Debug.Log("Shooting");
        //    if (alternateFire == 0)
        //    {
        //        bullets.SpawnFromPool("Bullet", leftBarrell.position, cam.forward, speed);
        //        alternateFire = 1;
        //    }else if (alternateFire == 1)
        //    {
        //        bullets.SpawnFromPool("Bullet", rightBarrell.position, cam.forward, speed);
        //        alternateFire = 0;
        //    }
        //}

    }    
    
    public void Shoot()
    {
        Debug.Log("Shooting");
        if (alternateFire == 0)
        {
            bullets.SpawnFromPool("Bullet", leftBarrell.position, cam.forward, 20f); // Loook up speed for the particular bullet being shot
            alternateFire = 1;
        }
        else if (alternateFire == 1)
        {
            bullets.SpawnFromPool("Bullet", rightBarrell.position, cam.forward, 20f);
            alternateFire = 0;
        }
    }
}
