using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        DataJuego.data.CargarData();
        SceneManager.LoadScene("House");
        if(DataJuego.data.vida<=0){
            DataJuego.data.vida = 5;
            DataJuego.data.bombas= 0;
            DataJuego.data.energia = 0;
            DataJuego.data.flechas = 0;
            DataJuego.data.rupias = 0;
        }
        
    }
 public void OnBackClick()
    {
        DataJuego.data.CargarData();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
