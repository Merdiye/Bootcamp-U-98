using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    // Spawn edilecek nesne prefab'i
    public GameObject objectToSpawn;

    // Spawn noktalarýnýn listesi
    public Transform[] spawnPoints;

    // Hangi spawn noktasýnda olduðumuzu takip eden deðiþken
    private int currentSpawnIndex = 0;

    // Maksimum nesne sayýsý
    public int maxObjects = 10;

    // Sahnedeki mevcut nesnelerin listesi
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Ýlk nesneleri spawn et
        for (int i = 0; i < maxObjects; i++)
        {
            SpawnObjectAtCurrentPoint();
        }
    }

    void Update()
    {
        // Sahnedeki nesneleri kontrol et ve yok olanlarý listeden çýkar
        spawnedObjects.RemoveAll(item => item == null);

        // Eðer mevcut nesne sayýsý maksimumdan az ise yeni nesne spawn et
        if (spawnedObjects.Count < maxObjects)
        {
            SpawnObjectAtCurrentPoint();
        }
    }

    void SpawnObjectAtCurrentPoint()
    {
        // Eðer spawn noktalarý bitti ise baþa dön
        if (currentSpawnIndex >= spawnPoints.Length)
        {
            currentSpawnIndex = 0;
        }

        // Nesneyi spawn et ve listeye ekle
        GameObject newObject = Instantiate(objectToSpawn, spawnPoints[currentSpawnIndex].position, spawnPoints[currentSpawnIndex].rotation);
        spawnedObjects.Add(newObject);

        // Bir sonraki spawn noktasýna geç
        currentSpawnIndex++;
    }
}
