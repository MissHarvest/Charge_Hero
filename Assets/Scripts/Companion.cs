using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    float a = 0;

    private void Start()
    {
        // player = GameManager.instance.go_Player;
    }

    private void Update()
    {
        Moving();
    }

    public void Moving()
    {
        a += 1 * Time.deltaTime;
        if (a > 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            a = 0;
        }
        else if(a > 0.5)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatus>().PowerUP(50);
            StageManager.Instance.rescue++;
            Destroy(this.gameObject);
        }
    }
}
