using UnityEngine;
using System.Collections;

public class CivilCarBehavior : MonoBehaviour
{

    public float crashDamage = 20f;
    public float civilCarSpeed = 5f;
    public int direction = -1;

    private Vector3 civilCarPosition;

    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, direction, 0) * civilCarSpeed * Time.deltaTime);
    }

    //czy tkniety jest najwiekszy box colidder
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.gameObject.GetComponent<PlayerCarMovement>().durability -= crashDamage / 5;
        }
    }

    //czy uderzony jest przod czy tyl cywila
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            //pobiera komponent zycie z PlayerCarMovement i odejmuje go
            obj.gameObject.GetComponent<PlayerCarMovement>().durability -= crashDamage;

            //Debug.Log("Gracz w nas wjechał");
            Destroy(gameObject);
        }
        else if (obj.gameObject.tag == "EndOfTheRoad")
        {
            Destroy(this.gameObject);
        }
    }

}
