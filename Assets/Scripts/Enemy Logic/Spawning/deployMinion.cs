using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployMinion : MonoBehaviour
{
    private ObjectPooler objPooler;

    // Start is called before the first frame update
    void Start()
    {
        objPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        objPooler.SpawnFromPool("Minion", transform.position);
        objPooler.SpawnFromPool("Brute", transform.position);
        objPooler.SpawnFromPool("Humanoid", transform.position);
    }
}
