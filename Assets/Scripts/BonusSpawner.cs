using UnityEngine;
using System.Collections;

public class BonusSpawner : MonoBehaviour
{
    //tablica obejktow, moze byc tam shield, dodatkowa wytrzymalosc, speed
    public GameObject[] bonuses;
    // wartosci ustawiamy w inspektorze
    public int minDelay;
    public int maxDelay;
    //w jakim czasie beda pojawiac sie bonusy
    private float delay;
	
    void Start()
    {
        //losowo wybierz z przedzialu
        delay = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            delay = Random.Range(minDelay, maxDelay);
            SpawnBonus();
        }
    }

    void SpawnBonus()
    {
        Instantiate(bonuses[(int)Random.Range(0, 3)], new Vector3(Random.Range(-2.3f,2.3f), 6f, 0), Quaternion.identity);

    }
}
