
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaveManager : MonoBehaviour
{

    [Header("Wave 1 (Civil Cars)")]
    public GameObject civilCar;
    //odstep czasu w ktorym pojazdy sie spawnuja
    public float civilCarSpawnDelay;
    public int civilCarsAmount;
    [Header("Wave 2 (Bandit Cars)")]
    public GameObject banditCar;
    public int bombsAmount;
    public int banditCarVerticalSpeed;
    public int banditCarHorizontalSpeed;
    public float bombDelay;
    private GameObject spawnedBanditCar;
    private bool isSpawned;
    private bool is2ndSpawned;
    [Header("Wave 3 (Police Cars)")]
    public GameObject policeCar;
    public int policeCarAmount;
    //statyczne by mozna bylo odwolac sie z innego miejsca
    [HideInInspector]
    static public bool isLeft;
    [HideInInspector]
    static public bool isRight;
    public float shootingSeriesDelay;
    public float singleShotDelay;
    public float policeCarVerticalSpeed;
    public int bulletsInSeries;
    private GameObject spawnedPoliceCar;

    [Header("Points")]
    public int pointsPerCivilCar;
    public int pointsPerBanditCar;
    public int pointsPerBomb;
    public int pointsPerPoliceCar;

    //miejsca startu pojazdow cywilnych
    private float[] lanesArray;
    //pomocnicza zmienna
    private float spawnDelay;

    void Start()
    {
        lanesArray = new float[4];
        lanesArray[0] = -2.11f;
        lanesArray[1] = -0.76f;
        lanesArray[2] = 0.76f;
        lanesArray[3] = 2.11f;
        spawnDelay = civilCarSpawnDelay;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PointsManager.points = 0;
            SceneManager.LoadScene(0);       
        }
        //ta funkcja miala konczyc gre ale cos nie trybi do konca
        if (GetComponent<CarDurabilityManager>().lifes <= 0 || (spawnedPoliceCar == null && policeCarAmount <= 0))
        {
            StartCoroutine("EndGame");
        }

        

        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0 && civilCarsAmount > 0)
        {
            //zeby sie zapetlalo
            spawnCar();
            spawnDelay = civilCarSpawnDelay;
        }
        else if (civilCarsAmount <= 0 && is2ndSpawned == false)
        {
            if (isSpawned == false)
            {
                spawnBanditCar();
            }
            else if (isSpawned == true && spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount < 5 && is2ndSpawned == false)
            {
                spawnBanditCar();
            }
        }
        else if (civilCarsAmount <= 0 && policeCarAmount > 0 && spawnedBanditCar == null)
        {
            spawnPoliceCar();
        }
    }

    IEnumerator EndGame()
    {
        new WaitForSeconds(5);
        PointsManager.points = 0;
        SceneManager.LoadScene(0);
        yield return null;
    }

    void spawnPoliceCar()
    {
        //jest blad gdy gracz jest untouchable lub gracz ma tag shield, mozna by zrobic dodatkowe warunki, ale czy warto?
        if (GameObject.FindWithTag("Player").gameObject.transform.position.x <= -0.51f && isRight == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(2.05f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = false;
            isRight = true;
            policeCarAmount--;
        }
        else if (GameObject.FindWithTag("Player").gameObject.transform.position.x > -0.51f && isLeft == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(-2.05f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = true;
            isLeft = true;
            policeCarAmount--;
        }
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().shootingSeriesDelay = shootingSeriesDelay;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().singleShotDelay = singleShotDelay;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().bulletsInSeries = bulletsInSeries;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().policeCarVerticalSpeed = policeCarVerticalSpeed;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().pointsPerCar = pointsPerPoliceCar;
    }

    void spawnBanditCar()
    {
        if (isSpawned == false)
        {
            spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(Random.Range(-2.25f, 2.25f), 7f, 0), Quaternion.identity);
            isSpawned = true;
        }
        else if (isSpawned == true && is2ndSpawned == false)
        {
            if (spawnedBanditCar.transform.position.x < 0.45f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(2.2f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            }
            else if (spawnedBanditCar.transform.position.x >= 0.45f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(-2.2f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            }
        }
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount = bombsAmount;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarVerticalSpeed = banditCarVerticalSpeed;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarHorizontalSpeed = banditCarHorizontalSpeed;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombDelay = bombDelay;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().pointsPerCar = pointsPerBanditCar;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().bomb.GetComponent<Bomb>().pointsPerBomb = pointsPerBomb;
    }

    void spawnCar()
    {
        int lane = Random.Range(0, 4);
        if (lane == 0 || lane == 1)
        {
            //funkcja Instantiate tworzy objekty//katy Eulera, 180 stopni obraca obrazek civil cara
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            //jesli nie zmienimy direction pojazd pojedzie do gory
            car.GetComponent<CivilCarBehavior>().direction = 1;
            car.GetComponent<CivilCarBehavior>().civilCarSpeed = 12f;
            car.GetComponent<CivilCarBehavior>().pointsPerCar = pointsPerCivilCar;
            
        }
        if (lane == 2 || lane == 3)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.identity);
            car.GetComponent<CivilCarBehavior>().pointsPerCar = pointsPerCivilCar;
        }
        civilCarsAmount--;
    }

}
