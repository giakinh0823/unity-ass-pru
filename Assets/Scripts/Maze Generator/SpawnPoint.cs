using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    public GameObject[] objectsToSpawn;

    private void Start()
    {
        if(objectsToSpawn !=null && objectsToSpawn.Length> 0)
        {
            int rand = Random.Range(0, objectsToSpawn.Length);
            GameObject instance = Instantiate(objectsToSpawn[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
    }
}
