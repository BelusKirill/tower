using UnityEngine;
using UnityEngine.Networking;

public class Test : NetworkBehaviour
{
    public RectTransform test;
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Хуй");
            test.Translate(0,01f,0);
        }
    }
}
