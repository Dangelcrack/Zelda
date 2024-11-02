using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject gameOverScreen;
    private GameObject player;
    private bool hasCompletedDialogue; // Nueva variable para evitar repetir el diálogo

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hasCompletedDialogue = false; // Inicializa como falso al comenzar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && !hasCompletedDialogue)
        {
            if (dialogPanel.activeInHierarchy)
            {
                if (dialogueText.text == dialogue[index])
                {
                    NextLine();
                }
                else
                {
                    StopTyping();
                }
            }
            else
            {
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    public void StopTyping()
    {
        dialogueText.text = dialogue[index];
        StopAllCoroutines();
    }

    public void zeroText()
    {
        if (dialogueText != null && dialogPanel != null)
        {
            dialogueText.text = "";
            index = 0;
            dialogPanel.SetActive(false);
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            if (gameObject.name == "Zelda")
            {
                hasCompletedDialogue = true; // Marca el diálogo como completado
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        gameOverScreen.SetActive(true);
        
        var playerMovement = player.GetComponent<Movement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasCompletedDialogue) // Verifica si el diálogo ya fue completado
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
