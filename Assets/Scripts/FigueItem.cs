using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigueItem : MonoBehaviour
{
    public int PosY;
    public int Drag;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().drag = Drag;
        //CheckHeight();
    }

    //вызывается несколько раз нужно пофиксить
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!tag.Equals("InCollision"))
        {
            GetComponent<Rigidbody2D>().mass = 5;
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            Invoke("NewItem", 0.2f);
        }
    }

    void NewItem()
    {
        PosY = (int)transform.position.y;
        gameObject.tag = "InCollision";
        gameObject.layer = 0;
    }
}
