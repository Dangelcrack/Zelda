using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool playerInRange; // Cambiado a "private" para mayor encapsulamiento
    
    void Start()
    {
        // Puedes inicializar cualquier otra lógica aquí si es necesario
    }

    // Update is called once per frame
    void Update()
    {
        // Verificamos que el jugador está en el rango y que se presiona espacio
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);   
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Solo establece playerInRange en true si el objeto tiene el tag "Player"
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Solo establece playerInRange en false y oculta el cuadro de diálogo si el objeto es el "Player"
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
