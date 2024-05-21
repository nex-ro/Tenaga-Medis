using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Objek yang akan di-spawn
    public Transform spawnPoint; // Titik di mana objek akan di-spawn
    public float minTime = 1.0f; // Waktu minimal antara spawn
    public float maxTime = 5.0f; // Waktu maksimal antara spawn

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            // Tunggu waktu acak antara minTime dan maxTime
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            // Spawn objek di spawnPoint
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
