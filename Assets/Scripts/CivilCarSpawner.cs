using UnityEngine;
using System.Collections;

public class CivilCarSpawner : MonoBehaviour
{
    //odstep czasu losowania nowych pojazdow
    public float carSpawnDelay = 2f;
    //tam wrzucimy objekt civilCar
    public GameObject civilCar;

    //pomocnicza zmienna
    private float spawnDelay;

    void Start()
    {
        spawnDelay = carSpawnDelay;
    }

    void Upadate()
    {
        
        spawnDelay -= Time.deltaTime;
        if(spawnDelay <= 0)
        {
            //zeby sie zapetlalo
            spawnCar();
            spawnDelay = carSpawnDelay;
        }
    }

    public void spawnCar()
    {
        //funkcja instantiate tworzy objekty
        Instantiate(civilCar, new Vector3(0.76f, 6f, 0), Quaternion.identity);

    }

}
