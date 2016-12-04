using UnityEngine;
using System.Collections;

public class CivilCarBehavior : MonoBehaviour
{
    //predkosc poruszania sie aut cywilnych
    public float civilCarSpeed = 5f;
    public float direction = -1f;
    //pozycja civil cara
    private Vector3 civilCarPosition;

    void Update()
    {
        //ten objekt do ktorego jest przypisany skrypt/Translate - zmienia polozenie
        this.gameObject.transform.Translate(new Vector3(0, direction, 0) * civilCarSpeed * Time.deltaTime);

    }

    //funkcja wywolana jesli wjedziemy w triggera/Colider obejektu ktory wjechal w tego triggera
    void OnTriggerEnter2D(Collider2D obj)
    {
        
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("Gracz w nas wjechał");
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "EndOfTheRoad")
        {
            Destroy(this.gameObject);
        }

    }

	
}
