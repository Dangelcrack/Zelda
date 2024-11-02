using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recoleccion : Colisiones
{
    public int idObjeto;
    public int delay;

    protected override void OnCollide(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            SoundEffectManager.Play("Item");
            switch (idObjeto)
            {
                case 1:
                    if (DataJuego.data.energia != 100)
                    {
                        DataJuego.data.energia += 1;
                    }
                    break;
                case 2:
                    if (DataJuego.data.vida != 9)
                    {
                        DataJuego.data.vida += 1;
                    }
                    break;
                case 3:
                    DataJuego.data.rupias += 1;
                    break;
                case 4:
                    DataJuego.data.bombas += 1;
                    break;
                case 5:
                    DataJuego.data.flechas += 5;
                    break;
                default:
                    break;
            }
            if(delay == 0){
                Destroy(gameObject);

            }else{
                StartCoroutine(waitTime());

            }
            Destroy(gameObject);
        }
    }
    public IEnumerator waitTime(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
