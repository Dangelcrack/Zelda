using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizaHud : MonoBehaviour
{
    public Text txRupias;
    public Text txBomba;
    public Text txFlecha;
    public GameObject energiaContainer;
    private float energiaTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txRupias.text = "" + DataJuego.data.rupias;
        txBomba.text = "" + DataJuego.data.bombas;
        txFlecha.text = "" + DataJuego.data.flechas;
        CalculaMagia();
    }

    private void CalculaMagia()
    {
        // Cálculo de la fracción de energía actual en relación con la energía máxima
        energiaTotal = (float)DataJuego.data.energia / DataJuego.data.maxEnergia;
        
        // Actualiza el fillAmount del componente Image en energiaContainer
        energiaContainer.GetComponent<Image>().fillAmount = energiaTotal;
    }
}
