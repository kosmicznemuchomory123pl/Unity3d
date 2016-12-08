using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndMenuManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PointsManager.points = 0;
            SceneManager.LoadScene(0);
        }
    }
    
	public void RestartButton()
    {
        PointsManager.points = 0;
        SceneManager.LoadScene(1);
    }
    public void ExitButton()
    {
        PointsManager.points = 0;
        SceneManager.LoadScene(0);
    }

}
