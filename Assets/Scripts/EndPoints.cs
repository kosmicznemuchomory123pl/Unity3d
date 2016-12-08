using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndPoints : MonoBehaviour
{
    
    
    void Start()
    {
        Text footext = GetComponent<Text>();
        footext.text = PointsManager.points.ToString();

    }
}
