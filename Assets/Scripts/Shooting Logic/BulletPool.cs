using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [System.Serializable]
    public class Bullet
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static BulletPool Instance;

    // On Script load
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Bullet> bullets;

    // Key, What you're storing <- That order
    public Dictionary<string, Queue<GameObject>> bulletDictionary;

    // Start is called before the first frame update
    void Start()
    {
        bulletDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Bullet bullet in bullets)
        {
            Queue<GameObject> bulletPool = new Queue<GameObject>();

            for (int i = 0; i < bullet.size; i++)
            {
                GameObject obj = Instantiate(bullet.prefab);
                obj.SetActive(false);
                bulletPool.Enqueue(obj);
            }

            bulletDictionary.Add(bullet.tag, bulletPool);
        }
    }

    public void Spawn()
    {
        // Spawn a mech -> Edit this to be any item -> Maybe Random later?
        //GameObject spawner;
        //spawner = GameObject.Find("Left Barrell");
        //SpawnFromPool("Bullet", spawner.transform.position);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Vector3 direction, float speed)
    {
        // Checks if tag exists before getting tag
        if (!bulletDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = bulletDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        // despite direction - add speed. if rest are zero it will return 0 if multiplied by zero
        Vector3 force = new Vector3(direction.x * speed, direction.y * speed, direction.z * speed);

       objectToSpawn.GetComponent<Rigidbody>().velocity = force;
       //objectToSpawn.GetComponent<Rigidbody>().velocity = direction * speed;

        bulletDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
