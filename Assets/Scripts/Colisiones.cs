using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public ContactFilter2D filtro;
    protected BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    protected virtual void Start()
    {
      boxCollider = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       boxCollider.OverlapCollider(filtro,hits);
       for(int i = 0 ; i < hits.Length; i++){
        if(hits[i]== null){
            continue;
        }
        OnCollide(hits[i]);
        hits[i]= null;
       }
    }

    protected virtual void OnCollide(Collider2D collider2D)
    {
        
    }
}
