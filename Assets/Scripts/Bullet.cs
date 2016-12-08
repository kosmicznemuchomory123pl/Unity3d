using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    public int direction;
    public float bulletSpeed;


    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(direction, 0, 0) * bulletSpeed * Time.deltaTime);
    }
	

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            
            obj.gameObject.GetComponent<PlayerCarMovement>().durability -= bulletDamage;
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "Shield" || obj.gameObject.tag == "Barrier" || obj.gameObject.tag == "Police")
        {
            Destroy(this.gameObject);
        }
    }
}
