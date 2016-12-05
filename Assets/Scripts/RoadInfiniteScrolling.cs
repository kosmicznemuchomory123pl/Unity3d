using UnityEngine;
using System.Collections;

public class RoadInfiniteScrolling : MonoBehaviour
{
    //skrypt jest przypisany do Drogi
    //http://stackoverflow.com/questions/36947732/unity-texture-offset-not-working
    //predkosc przewijania drogi
    public float scrollSpeed;
    //offset ktory nalozylismy na teksture objektu 3 wymiarowego
    private Vector2 offset;

	
	// Update is called once per frame
	void Update ()
    {
        offset = new Vector2(0, Time.time * scrollSpeed);
        //ta funkcja pobierze komponent z objektu do ktorego skrypt jest przypisany
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
