using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToSpawn
{
    public GameObject item;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public ItemToSpawn[] itemToSpawn;

    private void Start()
    {
        for(int i = 0; i < itemToSpawn.Length;i++)
        {
            if(i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1; //60 - 1 = 59
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1; //79 + 1 = 80
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;// 80 + 10 = 90 - 1 = 89
            }
        }
        Spawnner();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Spawnner();
        }
    }

    private void Spawnner()
    {
        float randomNumber = Random.Range(0, 100);

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (randomNumber >= itemToSpawn[i].minSpawnProb && randomNumber <= itemToSpawn[i].maxSpawnProb)
            {
                Debug.Log(randomNumber + " " + itemToSpawn[i].item);

                Instantiate(itemToSpawn[i].item, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
