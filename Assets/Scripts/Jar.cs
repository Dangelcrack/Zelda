using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    public int typeJar;
    public int roto;
    public int idEvent; 
    public List<GameObject> objetos = new List<GameObject>();
    public Animator aim;
    public Collider2D boxCollider;
    private bool playerInRange;

    void Awake()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<Collider2D>();
        }
    }

    void Start()
    {
        if (aim == null)
        {
            aim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Solo permite la interacción si el jugador está en rango, la jarra no ha sido rota, y se presiona la tecla X
        if (playerInRange && Input.GetKeyDown(KeyCode.X) && roto == 0)
        {
            jarInteraction();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Cambiado a público para permitir acceso desde PlayerHit
    public void jarInteraction()
    {
        // Verifica si la jarra ya está rota; si lo está, sale de la función
        if (roto == 1)
        {
            return;
        }

        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        roto = 1;
        SoundEffectManager.Play("Jar");
        yield return new WaitForSeconds(0); // Ajusta según duración de la animación
        aim.SetInteger("Usado", roto);

        // Deshabilita el collider
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        // Crea el objeto basado en el tipo de jarra
        if (typeJar == 1)
        {
            int x = Random.Range(0, objetos.Count);
            Instantiate(objetos[x], transform.position, transform.rotation);
        }
        else if (typeJar >= 0 && typeJar < objetos.Count) // Validación para evitar errores de índice
        {
            Instantiate(objetos[typeJar], transform.position, transform.rotation);
        }
    }
}
