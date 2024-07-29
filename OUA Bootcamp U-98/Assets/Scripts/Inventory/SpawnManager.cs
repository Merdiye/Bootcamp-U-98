using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    // Spawn edilecek nesne prefab'i
    public GameObject objectToSpawn;

    // Spawn noktalar�n�n listesi
    public Transform[] spawnPoints;

    // Hangi spawn noktas�nda oldu�umuzu takip eden de�i�ken
    private int currentSpawnIndex = 0;

    // Maksimum nesne say�s�
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
        // �lk nesneleri spawn et
        for (int i = 0; i < maxObjects; i++)
        {
            SpawnObjectAtCurrentPoint();
        }
    }*/

    void Update()
    {
        // Sahnedeki nesneleri kontrol et ve yok olanlar� listeden ��kar
        spawnedObjects.RemoveAll(item => item == null);

        // E�er mevcut nesne say�s� maksimumdan az ise yeni nesne spawn et
        if (spawnedObjects.Count < maxObjects && cooldownBool)
        {
            SpawnObjectAtCurrentPoint();
            StartCoroutine(SpawnCooldown());
        }
    }

    void SpawnObjectAtCurrentPoint()
    {
        // E�er spawn noktalar� bitti ise ba�a d�n
        if (currentSpawnIndex >= spawnPoints.Length)
        {
            currentSpawnIndex = 0;
        }

        // Nesneyi spawn et ve listeye ekle
        GameObject newObject = Instantiate(objectToSpawn, spawnPoints[currentSpawnIndex].position, spawnPoints[currentSpawnIndex].rotation);
        spawnedObjects.Add(newObject);

        // Bir sonraki spawn noktas�na ge�
        currentSpawnIndex++;
    }

    private IEnumerator SpawnCooldown()
    {
        cooldownBool = false;
        yield return new WaitForSeconds(cooldownPeriod);
        cooldownBool = true;
    }
}
