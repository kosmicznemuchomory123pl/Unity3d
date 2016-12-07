using UnityEngine;
using System.Collections;

public class PoliceCarBehaviour : MonoBehaviour
{

    public Light redLight;
    public Light blueLight;
    public float lightDelay;
    public GameObject bullet;
    //opoznienie serii
    public float shootingSeriesDelay;
    //opoznienie poj. strzalu
    public float singleShotDelay;
    //zmienna okresla czy pojazd gracza jest po lewej czy po prawej, umozliwia to spawnowanie po przeciwnej stronie
    //pojazd policyjny wie tez w ktora strone strzelac
    public bool isLeft;
    public float policeCarVerticalSpeed;
    public int bulletsInSeries;

    private float lightShowDelay;
    private float shootDelay;
    private Vector3 policaCarPos;
    private GameObject bulletObj;

    void Start()
    {
        lightShowDelay = 2 * lightDelay;
        shootDelay = shootingSeriesDelay;
    }

    void Update()
    {
        lightShowDelay -= Time.deltaTime;

        if(lightShowDelay>lightDelay)
        {
            blueLight.enabled = true;
            redLight.enabled = false;
        }
        else if (lightShowDelay <= lightDelay && lightShowDelay > 0)
        {
            blueLight.enabled = false;
            redLight.enabled = true;
        }
        else if (lightShowDelay <= 0)
        {
            lightShowDelay = 2 * lightDelay;
        }

        if (gameObject.transform.position.y < -3.2f)
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0) * policeCarVerticalSpeed * Time.deltaTime);
        }
        else
        {
            shootDelay -= Time.deltaTime;
            if (shootDelay <= 0)
            {
                StartCoroutine("Shoot");
                shootDelay = shootingSeriesDelay;
            }
        }
    }

    IEnumerator Shoot()
    {
        for (int i = bulletsInSeries; i > 0; i--)
        {
            bulletObj = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            if (isLeft == true)
            {
                bulletObj.GetComponent<Bullet>().direction = 1;
            }
            else if (isLeft == false)
            {
                bulletObj.GetComponent<Bullet>().direction = -1;
            }
            yield return new WaitForSeconds(singleShotDelay);
        }
    }

    void OnCollisionEnter2D (Collision2D obj)
    {
        if(obj.gameObject.tag == "Barrier")
        {
            Destroy(this.gameObject);
        }
    }
	
}
