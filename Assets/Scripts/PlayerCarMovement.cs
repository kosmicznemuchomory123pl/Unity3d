﻿using UnityEngine;
using System.Collections;

public class PlayerCarMovement : MonoBehaviour {

    //predkosc poruszania sie na boki
    public float carHorizontalSpeed = 3f;
    //pozycja naszego samochodu
    private Vector3 carPosition;


    void Start()
    {
        carPosition = this.gameObject.transform.position;
    }

    void Update()
    {
        //.x oznacza ze dzialamy tylko w poziomie. "Horizontal" wbudownae w unity.
        carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpeed * Time.deltaTime;

        carPosition.x = Mathf.Clamp(carPosition.x, -2.41f, 2.41f);
        //do naszego objektu gry, do jego pozycji przypisujemy pozycje carPosition
        this.gameObject.transform.position = carPosition;
    }

}
