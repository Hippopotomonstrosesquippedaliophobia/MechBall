using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployMinion : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private ObjectPooler objPooler;

    // Start is called before the first frame update
    void Start()
    {
        objPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        objPooler.SpawnFromPool("Minion", spawnPoint.position);
        objPooler.SpawnFromPool("Brute", spawnPoint.position);
    }
}
