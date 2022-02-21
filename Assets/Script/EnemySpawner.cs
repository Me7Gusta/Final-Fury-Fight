using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GoUI goUI;

    public EnemiesInSpawn[] enemies;

    private CameraFollow cam;

    public float minZ, maxZ;
    private int i = 0;
    private int enemyCount = 0;
    private int numberOfEnemies = 0;
    private int currentEnemies = 0;
    private float spawnTime = 0.5f;
    private float camMaxX;
    private float minX, maxX;
    private int enemiesNum;

    private void Awake()
    {
        //camera = FindObjectOfType<CameraFollow>();
        cam = Camera.main.GetComponent<CameraFollow>();

        for(int i = 0; i < enemies.Length; i++)
        {
            numberOfEnemies += enemies[i].number;
        }
    }

    private void Update()
    {
        if (currentEnemies >= numberOfEnemies)
        {
            enemiesNum = FindObjectsOfType<Enemy>().Length;

            if(enemiesNum <= 0)
            {
                goUI.ActivateGoUI();
                cam.maxXAndY.x = camMaxX;
                gameObject.SetActive(false);
            }
        }
    }

    private void SpawnEnemy()
    {
        bool positionX = Random.Range(0, 2) == 0 ? true : false;
        
        Vector3 spawnPosition;
        spawnPosition.z = Random.Range(minZ, maxZ);

        if (positionX)
        {
            spawnPosition = new Vector3(maxX, 0, spawnPosition.z);
        }
        else
        {
            spawnPosition = new Vector3(minX, 0, spawnPosition.z);
        }

        
        Instantiate(enemies[i].enemy, spawnPosition, Quaternion.identity);

        enemyCount++;

        currentEnemies++;
        
        if (enemyCount >= enemies[i].number)
        {
            i++;
            enemyCount = 0;
        }

        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;

            camMaxX = cam.maxXAndY.x;
            cam.maxXAndY.x = transform.position.x;

            float distanceZ = (transform.position - Camera.main.transform.position).z;
            minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ)).x - 12;
            maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ)).x + 12;

            SpawnEnemy();
        }
    }
}
