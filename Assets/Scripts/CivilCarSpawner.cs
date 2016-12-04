using UnityEngine;
using System.Collections;

public class CivilCarSpawner : MonoBehaviour
{
    //odstep czasu losowania nowych pojazdow
    public float carSpawnDelay = 2f;
    //tam wrzucimy objekt civilCar
    public GameObject civilCar;

    //ilosc miejsc startu
    private float[] lanesArray;

    //pomocnicza zmienna
    private float spawnDelay;

    void Start()
    {
        lanesArray = new float[4];
        lanesArray[0] = -2.22f;
        lanesArray[1] = -0.76f;
        lanesArray[2] = 0.76f;
        lanesArray[3] = 2.22f;
        spawnDelay = carSpawnDelay;
    }

    void Update()
    {

        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0)
        {
            //zeby sie zapetlalo
            spawnCar();
            spawnDelay = carSpawnDelay;
        }
    }

    void spawnCar()
    {
        int lane = Random.Range(0, 4);
        if (lane == 0 || lane == 1)
        {
            //funkcja Instantiate tworzy objekty//katy Eulera, 180 stopni obraca obrazek civil cara
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

            car.GetComponent<CivilCarBehavior>().direction = 1;
            car.GetComponent<CivilCarBehavior>().civilCarSpeed = 12f;
        }
        if (lane == 2 || lane == 3)
        {
            Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.identity);
        }
    }

}
