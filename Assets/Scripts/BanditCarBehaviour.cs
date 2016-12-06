using UnityEngine;
using System.Collections;

public class BanditCarBehaviour : MonoBehaviour
{
    //obiekt bomby
    public GameObject bomb;
    public int bombsAmount;
    //V = y, H = x
    public int banditCarVerticalSpeed;
    public int banditCarHorizontalSpeed;
    //odstep czasu w jakim samochod bandytow wypuszcza bombe
    public float bombDelay;

    //opoznienie do odejmowania i odliczania
    private float Delay;
    //obiekt samochodu gracza, by auto bandytow wiedzialo za kim podazac
    private GameObject playerCar;
    //
    private Vector3 banditCarPos;


    void Start()
    {
        playerCar = GameObject.FindWithTag("Player");
        Delay = bombDelay;
    }

    void Update()
    {
        //jesli nie znalazlo obiektu gracza na starcie
        if(playerCar == null)
        {
            playerCar = GameObject.FindWithTag("Player");
        }
        else
        {
            if (gameObject.transform.position.y > 3.8f && bombsAmount > 0)
            {
                //ustawia pojazd bandytow w okreslonym miejscu
                this.gameObject.transform.Translate(new Vector3(0, -1, 0) * banditCarVerticalSpeed * Time.deltaTime);
            }
            else if (bombsAmount<=0)
            {
                this.gameObject.transform.Translate(new Vector3(0, 1, 0) * banditCarVerticalSpeed * Time.deltaTime);
                if(gameObject.transform.position.y > 6.5f)
                {
                    Destroy(this.gameObject);
                }
                    
            }
            //tutaj wykonuja sie polecenia podazania za graczem i zostawianie bomb
            else
            {
                //Lerp jest funkcja dzieki ktorej jeden obiekt porusza za innym z okreslonym czasem, by nie bylo tak, ze pojazd bandytow ma zawsze taki sam vektor jak pojazd gracza
                banditCarPos = Vector3.Lerp(transform.position, playerCar.transform.position, Time.deltaTime * banditCarHorizontalSpeed);
                //Wektor banditCarPos zmienia ciagle swoje dane, ale trzeba zmienic pozycje w swiecie gry dzieki ponizszemu poleceniu
                //zmieniamy dzieki banditCarPos.x tylko nasze wspolrzedne x, bo jesli zrobilisbysmy banditCarPos.y po pierwszym przecinku to pojazd zrownal by sie z pojazdem gracza
                transform.position = new Vector3(banditCarPos.x, transform.position.y, 0);

                Delay -= Time.deltaTime;
                
                //jesli zostalo kilka bomb to zostana wypuszczone z dwukrotna szybkoscia
                if (Delay <= 0 && bombsAmount <=5 && bombsAmount > 0)
                {
                    Delay = bombDelay/2;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
                else if (Delay <= 0 && bombsAmount > 0)
                {
                    Delay = bombDelay;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
            }

        }
    }

    

	
}
