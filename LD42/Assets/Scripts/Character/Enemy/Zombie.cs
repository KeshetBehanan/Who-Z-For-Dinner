using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private static Transform player;

    public static List<Zombie> zombiesAlive;

    public float speed = 0.5f;

    private bool isAlive;

    private void Awake()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if(zombiesAlive == null)
        {
            zombiesAlive = new List<Zombie>();
        }
    }

    private void Start()
    {
        zombiesAlive.Add(this);
        isAlive = true;
    }

    private void Update()
    {
        if(transform.position.y < -1)
        {
            zombiesAlive.Remove(this);
        }
        if(isAlive)
        {
            if(player == null)
            {
                return;
            }
            var dir = (player.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Player") && isAlive)
        {
            Kill();
        }
    }

    public void Kill()
    {
        transform.rotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
        transform.Translate(Vector3.up * 0.5f, Space.World);
        zombiesAlive.Remove(this);
        isAlive = false;
        gameObject.layer = 10;
        GameManager.instance.Increase();
        Destroy(gameObject, 3f);
    }
}
