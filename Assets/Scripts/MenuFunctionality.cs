using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuFunctionality : MonoBehaviour
{



   
    

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void HighScoresButton()
    {
        //SceneManager.LoadScene(2);
    }
    public void OptionButton()
    {
        //SceneManager.LoadScene(3);
    }
    public void ExitButton()
    {
        Application.Quit();
    }


}
