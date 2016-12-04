using UnityEngine;
using System.Collections;

public class CarDurabilityManager : MonoBehaviour
{
    //wrzucamy tu pojazd gracza
    public GameObject playerCarPrefab;
    //ustawiamy tam miejsce spawnowania pojazdu
    public GameObject spawnPoint;
    public TextMesh durabilityText;
    public int lifes;
    //
    private GameObject playerCar;

    void Start()
    {
        //stworzenie gracza //playerCar potrzebne do tego by moc operowac na nowo stworzonym pojezdzie
        playerCar = (GameObject) Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    void Update()
    {
        if(playerCar.GetComponent<PlayerCarMovement>().durability <= 0)
        {
            Destroy(playerCar);
            lifes--;
            if(lifes > 0)
            {
                //tworzenie corutyny //https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
                //corutyny wywoluja sie i pracuja niezaleznie //nie mozemy zatrzymac czasu w funkcji update
                StartCoroutine("SpawnaCar");
            }
            //nie mozemy dzieki bonusom zwiekszyc wytrzymalosci wiekszej niz 100
            else if(playerCar.GetComponent<PlayerCarMovement>().durability > playerCar.GetComponent<PlayerCarMovement>().maxDurability)
            {
                playerCar.GetComponent<PlayerCarMovement>().durability = playerCar.GetComponent<PlayerCarMovement>().maxDurability;
            }
        }
        durabilityText.text = "WYTRZYMALOSC: " + playerCar.GetComponent<PlayerCarMovement>().durability + "/" + playerCar.GetComponent<PlayerCarMovement>().maxDurability;
    }

    //spawnowanie objektu i dawanie mu 3 sekund niezniszczalnosci
    IEnumerator SpawnaCar()
    {

        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = true;
        playerCar.tag = "Untouchable";

        //funkcja czeka 3 sekundy
        yield return new WaitForSeconds(3);

        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        playerCar.tag = "Player";
    }




}
