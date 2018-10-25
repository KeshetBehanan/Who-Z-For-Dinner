using EZObjectPools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float delay = 4;
    public EZObjectPool pool;

    [SerializeField]
    private Transform shootPosition;
    private Vector3 dir;

    private float lastShoot;

    private void Update()
    {
        if(Input.GetMouseButton(0) && lastShoot + delay > Time.time)
        {
            //var zom = GetClosestZombie();
            //if(zom != null)
            //{
            //    lastShoot = Time.time;

            //    var pos = zom.transform.position;
            //    pos.y = shootPosition.position.y;

            //    var dir = (pos - shootPosition.position).normalized;

            //    transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            //    GameObject bullet;
            //    pool.TryGetNextObject(shootPosition.position, transform.rotation, out bullet);
            //    bullet.GetComponent<Bullet>().SetVelocity(dir);
            //}

            //
            // NEW WAY 
            //
            lastShoot = Time.time;

            GameObject bullet;
            pool.TryGetNextObject(shootPosition.position, transform.rotation, out bullet);
            bullet.GetComponent<Bullet>().SetVelocity(dir);
        }
    }


    private GameObject GetClosestZombie()
    {
        GameObject closest = null;
        float minDis = float.MaxValue;
        foreach(var zom in Zombie.zombiesAlive)
        {
            var dis = (zom.transform.position - shootPosition.position).sqrMagnitude;
            if(minDis > dis)
            {
                minDis = dis;
                closest = zom.gameObject;
            }
        }
        return closest;
    }
}
