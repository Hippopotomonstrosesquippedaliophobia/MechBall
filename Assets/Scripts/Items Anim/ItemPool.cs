using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ItemPool Instance;

    // On Script load
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Item> items;

    // Key, What you're storing <- That order
    public Dictionary<string, Queue<GameObject>> itemDictionary;

    // Start is called before the first frame update
    void Start()
    {
        itemDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Item item in items)
        {
            Queue<GameObject> itemPool = new Queue<GameObject>();

            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                itemPool.Enqueue(obj);
            }

            itemDictionary.Add(item.tag, itemPool);
        }

        //Spawns a Mech FOR NOW
        Spawn();
    }

    public void Spawn()
    {
        // Spawn a mech -> Edit this to be any item -> Maybe Random later?
        GameObject itemSpawner;
        itemSpawner = GameObject.Find("Item Spawner");
        SpawnFromPool("Mech", itemSpawner.transform.position);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        // Checks if tag exists before getting tag
        if (!itemDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = itemDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        itemDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
