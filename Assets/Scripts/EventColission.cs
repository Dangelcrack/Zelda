using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventColission : Colisiones
{
    public string escena; 
    public Vector2 playerPosition;

    protected override void OnCollide(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            SceneManager.LoadScene(escena);
        }
    }
}