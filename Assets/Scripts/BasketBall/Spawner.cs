using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int time;
    public bool randomX;
    public bool stopSpawn = true;
    public enum EnemyType
    {
        enemy,
        enemy2,
        enemy3
    }
    void Start()
    {
        InvokeRepeating("Spawn", 0, time);
    }
    private void Spawn()
    {
        if (!stopSpawn)
        {
            MakeEnemy();
        }
    }
    private void MakeEnemy()
    {
        float randomNumberX = Random.Range(-0.238f, 0.19f);
        float randomNumberZ = Random.Range(-0.213f, 0.223f);
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(GetRandomTag());
        if (enemy != null)
        {
            if (randomX)
            {
                enemy.transform.position = new Vector3(randomNumberX, this.transform.position.y, this.transform.position.z);
                enemy.transform.rotation = this.transform.rotation;
                enemy.SetActive(true);
            }
            else
            {
                enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, randomNumberZ);
                enemy.transform.rotation = this.transform.rotation;
                enemy.SetActive(true);
            }
        }
    }
    private string GetRandomTag()
    {
        var enemyType = (EnemyType)Random.Range(0, 3);
        return enemyType.ToString();
    }
}