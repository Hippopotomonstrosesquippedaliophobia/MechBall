using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Items
{
    [SerializeField] private SphereCollider checkForPickup;

    // Start is called before the first frame update
    void Start()
    {
        thisItem = this.gameObject;
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
    }    
}
