using UnityEngine;
using System.Collections;

public class Bonuses : MonoBehaviour
{

    //jaki typ bonusu
    public bool isDurability;
    public bool isShield;
    public bool isSpeed;

    //Bonuses Settings
    public float bonusSpeed = 10f;

    //Durability Settings
    public float repairPoints;

    //Shield Settings
    //objekt ktory bedzie pokazywal ze mamy tarcze
    public GameObject shield;
    private GameObject playerCar;
    private Vector3 playerCarPos;

    //Speed Settings
    //z jaka predkoscia gra przyspiesza
    public float speedBoost;
    //czas trwania przyspieszenia
    public float duration;
    //bedzie potrzebne by nie mozna bylo przyspieszyc gry kilkukrotnie dzieki wielu bonusom
    private bool isActivated = false;

	void Update ()
    {
        //zmienia polozenie objektu, porusza sie jak samochod cywilow
        this.gameObject.transform.Translate(new Vector3(0, -1, 0) * bonusSpeed * Time.deltaTime);
        if (transform.position.y < -14f && isActivated == false)
        {
            Destroy(gameObject);
        }

	}

    void OnTriggerEnter2D(Collider2D obj)
    {
        //objekt ktory w nas wjechal czy rowna sie Player
        if (obj.gameObject.tag == "Player" || obj.gameObject.tag == "Shield")
        {
            if (isDurability == true)
            {
                obj.gameObject.GetComponent<PlayerCarMovement>().durability += repairPoints;
                Destroy(this.gameObject);
            }
            else if (isShield == true)
            {
                //przypisujemy do objektu playerCar wartosci wyszukane dzieki tagowi, jedynie nasz czerwony pojazd ma tag "Player"
                playerCar = GameObject.FindWithTag("Player");
                //robimy tak by np. pojazdy cywilow wiedzialy ze nie wjezdza w nich gracz tylko tarcza
                obj.gameObject.tag = "Shield";
                playerCarPos = playerCar.transform.position;
                playerCarPos.z = -0.1f;
                //to bedzie swiatelko :D
                GameObject shieldObj = (GameObject)Instantiate(shield, playerCarPos, Quaternion.identity);
                //zeby tarcza poruszala sie z samochodem, to musimy zmienic rodzica, czyli przypisujemy tarcze do pojazdu gracza
                shieldObj.transform.parent = playerCar.transform;
                //usuwamy bonus(ten znaczek) ze swiata gry
                Destroy(this.gameObject);
            }
            else if (isSpeed == true)
            {
                //zeby bonus nie byl widoczny w czasie jego wykonywania
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isActivated = true;
                StartCoroutine("SpeedBoostActivated");
                

            }
        }
        //nie chcialo dzialac wiec usunelismy obiekt w funkcji update
        /*else if (obj.gameObject.tag == "EndOfTheRoad" && isActivated == false)
        {
            Destroy(this.gameObject);
        }*/

    }

    IEnumerator SpeedBoostActivated()
    {
        while(duration > 0)
        {
            duration -= Time.deltaTime / speedBoost;
            //odpowiada za skale czasu, jesli bedzie ponizej 1 to czas zwolni a jesli wieksza od 1 to przyspieszy
            Time.timeScale = speedBoost;
            // kazda korutyna musi cos zwracac
            yield return null;
        }
        
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
