using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataJuego : MonoBehaviour
{
    public static DataJuego data;
    private String rutaArchivo;
    public float energia;
    public int vida;
    public int rupias;
    public int bombas;
    public int flechas;
    public float maxEnergia = 100f;
    public int maxVida = 9;

    // Variable para controlar la invulnerabilidad
    public bool isInvulnerable = false;

    [Serializable]
    class DatosGuardar
    {
        public float DTenergia;
        public int DTvida;
        public int DTrupias;
        public int DTbombas;
        public int DTflechas;
    }

    private void Awake()
    {
        rutaArchivo = Application.dataPath + "/data.ping";
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CargarData();
    }

    public void CargarData()
    {
        if (File.Exists(rutaArchivo))
        {
            Debug.Log("Se ha cargado la data del jugador");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(rutaArchivo, FileMode.Open);
            DatosGuardar dat = (DatosGuardar)bf.Deserialize(file);
            energia = dat.DTenergia;
            vida = dat.DTvida;
            rupias = dat.DTrupias;
            bombas = dat.DTbombas;
            flechas = dat.DTflechas;
            file.Close();
        }
    }

    public void GuardarData()
    {
        Debug.Log("Se ha guardado la partida");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);
        DatosGuardar dat = new DatosGuardar();
        dat.DTenergia = energia;
        dat.DTvida = vida;
        dat.DTrupias = rupias;
        dat.DTbombas = bombas;
        dat.DTflechas = flechas;
        bf.Serialize(file, dat);
        file.Close();
    }

    // Método para ganar vida
    public void GanarVida(int cantidad)
    {
        vida = Mathf.Clamp(vida + cantidad, 0, maxVida);
        // Llamada opcional a GuardarData() para guardar el cambio inmediatamente
        GuardarData();
    }

    // Método para perder vida
    public void PerderVida(int cantidad)
    {
        vida = Mathf.Clamp(vida - cantidad, 0, maxVida);
        // Llamada opcional a GuardarData() para guardar el cambio inmediatamente
        GuardarData();
    }
}
