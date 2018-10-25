using EZObjectPools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public Transform reletiveRotation;

    private float lastShoot;
    public float fireRate = 10;

    [SerializeField]
    private Transform shootPosition;
    public EZObjectPool pool;

    private void Update()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            var pos = hit.point;
            pos.y = shootPosition.position.y;

            var dir = pos - shootPosition.position;

            if(dir.magnitude > 1)
            {
                transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
            }

            if(Input.GetMouseButton(0) && lastShoot + (1 / fireRate) < Time.time)
            {
                lastShoot = Time.time;

                GameObject bullet;

                pool.TryGetNextObject(shootPosition.position, transform.rotation, out bullet);
                bullet.GetComponent<Bullet>().SetVelocity(dir.normalized);
            }
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), reletiveRotation);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxisRaw("Vertical"), reletiveRotation);
    }
}
