using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Spawner : MonoBehaviour
    {
        public void SpawnObject(GameObject objectToSpawn)
        {
            // Spawn a new object based on the spawner's settings
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
    }

