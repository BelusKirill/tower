using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MoveController : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> Items;
    public int ItemId;
    public int DragItems;
    public TextMeshProUGUI Height;
    public GameObject Menu;

    public GameObject gm;

    private Rigidbody2D item;
    private int MaxPosY = 0;
    private int CameraSise = 5;
    private bool paused = false;

    delegate void MyDelegate();

    MyDelegate myDelegate;

    void Start()
    {
        Instantiate(platform, new Vector3(0, -5, 0), platform.transform.rotation);
        item = Spawn();
        myDelegate = Move;
        StartCoroutine(ieEnumeratorHeight());
    }
    
    void Update()
    {
        IsPause();

        if (!paused)
        {

            if (item.tag.Equals("InCollision"))
            {
                item = Spawn();
            }

            gm.transform.position = item.transform.position;

            gm.transform.localScale = new Vector3(item.GetComponent<Collider2D>().bounds.size.x, 50, 1);

            Move();

            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, 4 + MaxPosY, transform.position.z), 0.01f);

            if (GetComponent<Camera>().orthographicSize < CameraSise)
            {
                GetComponent<Camera>().orthographicSize += 0.01f;
            }

        }
    }

    private Rigidbody2D Spawn()
    {
        ItemId = UnityEngine.Random.Range(0, Items.Count);
        GameObject boxSpawn =
            Instantiate(Items[ItemId], new Vector3(0, 12 + MaxPosY, 0), Items[ItemId].transform.rotation);
        boxSpawn.GetComponent<FigueItem>().Drag = DragItems;
        return boxSpawn.GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        if (Input.touchCount > 0 && IsNotPointerOverGameObject(Input.touches[0].fingerId) && Input.touches[0].phase == TouchPhase.Began)
        {
            GoMove(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Input.touchCount == 0)
        {
            GoMove(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void IsPause()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!paused)
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Paused()
    {
        Menu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    private void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    bool IsNotPointerOverGameObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        return (!eventSystem.IsPointerOverGameObject(fingerId)
                && eventSystem.currentSelectedGameObject == null);
    }

    private int CheckingForSign(float number)
    {
        if (number > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void GoMove(Vector3 pos)
    {
        //item.transform.position = Vector3.Lerp(item.transform.position,
        //    new Vector3(7 * CheckingForSign(pos.x), item.transform.position.y, item.transform.position.z),
        //    0.01f);
        item.transform.position = new Vector3(item.transform.position.x + CheckingForSign(pos.x) * 0.5f, item.transform.position.y, item.transform.position.z);
    }

    public void ButtonClickLeft()
    {
        item.rotation += 90;
    }

    public void ButtonClickRight()
    {
        if (paused) return;
        item.rotation -= 90;
    }

    public void ButtonClickDown()
    {
        if (paused) return;
        item.mass = 0.5f;
        item.gravityScale = 1;
    }

    public void ButtonResume()
    {
        Resume();
    }

    public void ButtonRestart()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    IEnumerator ieEnumeratorHeight()
    {
        while (true)
        {
            MaxPosY = CheckHeight();
            Height.SetText("Высота : " + MaxPosY);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public int CheckHeight()
    {
        int height = 0;
        for (float i = -5; i <= 5.5; i+=0.5f)
        {
            //пускаем луч
            RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(transform.position.x + i, transform.position.y + 5), Vector2.down);

            //если луч с чем-то пересёкся, то..
            if (hit2D.collider != null)
            {
                GameObject hitColliderGameObject = hit2D.collider.gameObject;
                //если луч не попал в цель
                if (hit2D.collider.tag.Equals("InCollision"))
                {
                    height = hitColliderGameObject.transform.position.y > height ? (int)hitColliderGameObject.transform.position.y : height;
                    Debug.Log("Путь к врагу преграждает объект: " + hit2D.collider.name + " | " + height + " i = " + i);

                    Debug.DrawLine(new Vector2(transform.position.x + i, transform.position.y + 5), new Vector2(transform.position.x + i, transform.position.y - 15), Color.green);
                }
                else
                {
                    Debug.DrawLine(new Vector2(transform.position.x + i, transform.position.y + 5), new Vector2(transform.position.x + i, transform.position.y - 15), Color.white);
                }
                //просто для наглядности рисуем луч в окне Scene
            }
            else
            {
                Debug.DrawLine(new Vector2(transform.position.x + i, transform.position.y + 5), new Vector2(transform.position.x + i, transform.position.y - 15), Color.red);
            }
        }
        return height;
    }
}
