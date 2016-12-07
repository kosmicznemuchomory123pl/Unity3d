using UnityEngine;
using System.Collections;

public class PointsManager : MonoBehaviour
{

    static public int points;
    private float secondDelay = 1;
    void Start()
    {
        
        this.gameObject.GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, 0.7f);

    }

    void Update()
    {
        this.gameObject.GetComponent<TextMesh>().text = points.ToString();
        secondDelay -= Time.deltaTime; 
        if (secondDelay <= 0)
        {
            points += 1;
            secondDelay = 1;
        }
    }
	
}
