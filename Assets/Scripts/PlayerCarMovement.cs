using UnityEngine;
using System.Collections;

public class PlayerCarMovement : MonoBehaviour {

    //predkosc poruszania sie na boki
    public float carHorizontalSpeed = 3f;
    //pozycja naszego samochodu
    private Vector3 carPosition;

    //moksymalne zycie pojazdu
    public float maxDurability = 100f;
    public float durability;


    void Start()
    {
        carPosition = this.gameObject.transform.position;

        durability = maxDurability;
    }

    void Update()
    {
        //.x oznacza ze dzialamy tylko w poziomie. "Horizontal" wbudownae w unity.
        carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpeed * Time.deltaTime;
        //Clamp jest to funkcja ktora trzyma nasz samochod w danych przedzialach,
        carPosition.x = Mathf.Clamp(carPosition.x, -2.41f, 2.41f);
        //do naszego objektu gry, do jego pozycji przypisujemy pozycje carPosition
        this.gameObject.transform.position = carPosition;
    }

}
