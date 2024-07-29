using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    public float cooldownPeriod;
    private bool cooldownBool;

    private void Awake()
    {
        cooldownBool = true;
    }
    /*void Start()
    {
        // Ýlk nesneleri spawn et
        for (int i = 0; i < maxObjects; i++)
        {
            SpawnObjectAtCurrentPoint();
        }
    }*/

    void Update()
    {
        // Sahnedeki nesneleri kontrol et ve yok olanlarý listeden çýkar
        spawnedObjects.RemoveAll(item => item == null);

        // Eðer mevcut nesne sayýsý maksimumdan az ise yeni nesne spawn et
        if (spawnedObjects.Count < maxObjects && cooldownBool)
        {
            SpawnObjectAtCurrentPoint();
            StartCoroutine(SpawnCooldown());
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

    private IEnumerator SpawnCooldown()
    {
        cooldownBool = false;
        yield return new WaitForSeconds(cooldownPeriod);
        cooldownBool = true;
    }
}
