using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    //czas trwania tarczy
    public float duration;

    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            //objekt rodzic czyli player car
            this.gameObject.transform.parent.tag = "Player";
            Destroy(this.gameObject);
        }
    }
	
}
