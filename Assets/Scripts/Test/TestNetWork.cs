using UnityEngine;
using UnityEngine.Networking;

public class TestNetWork : NetworkBehaviour
{
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void FixedUpdate()
    {
        if(!isLocalPlayer) return;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0,0.1f,0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.1f, 0);
        }
    }
}
