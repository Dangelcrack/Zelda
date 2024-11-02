using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    private List<Image> hearts = new List<Image>();

    private int lastHealth; // Para verificar cambios en la vida

    void Start()
    {
        // Configura el número máximo de corazones al iniciar
        SetMaxHearts(DataJuego.data.maxVida);
        UpdateHearts(DataJuego.data.vida);
        lastHealth = DataJuego.data.vida; // Inicializar con el valor actual
    }

    void Update()
    {
        // Solo actualizar si la vida ha cambiado
        if (DataJuego.data.vida != lastHealth)
        {
            UpdateHearts(DataJuego.data.vida);
            lastHealth = DataJuego.data.vida;
        }
    }

    public void SetMaxHearts(int maxHearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            newHeart.sprite = fullHeartSprite;
            newHeart.color = Color.red;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeartSprite;
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
                hearts[i].color = Color.white;
            }
        }
    }
}
