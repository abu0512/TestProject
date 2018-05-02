using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    private List<Bullet> _bullets = new List<Bullet>();
    private List<StunBullet> _stunBullets = new List<StunBullet>();

    void Awake()
    {
        foreach (Bullet b in GetComponentsInChildren<Bullet>())
        {
            b.gameObject.SetActive(false);
            _bullets.Add(b);
        }

        foreach (StunBullet s in GetComponentsInChildren<StunBullet>())
        {
            s.gameObject.SetActive(false);

            _stunBullets.Add(s);
        }
    }

    public Bullet SetBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        Bullet bullet = GetBullet();
        bullet.InitBullet(queen, from, target);

        return bullet;
    }

    public StunBullet SetStunBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        StunBullet stunbullet = GetStunBullet();
        stunbullet.InitStunBullet(queen, from, target);

        return stunbullet;
    }

    private Bullet GetBullet()
    {
        Bullet bullet = null;

        foreach (Bullet b in _bullets)
        {
            if (b.gameObject.activeInHierarchy)
                continue;

            bullet = b;
        }

        if (bullet == null)
            bullet = CreateBullet();

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    private StunBullet GetStunBullet()
    {
        StunBullet stunbullet = null;

        foreach (StunBullet s in _stunBullets)
        {
            if (s.gameObject.activeInHierarchy)
                continue;

            stunbullet = s;
        }

        if (stunbullet == null)
            stunbullet = CreateStunBullet();

        stunbullet.gameObject.SetActive(true);

        return stunbullet;
    }

    private StunBullet CreateStunBullet()
    {
        StunBullet stunbullet = null;
        GameObject stunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/StunBullet");
        stunbullet = Instantiate(stunbulletObject).GetComponent<StunBullet>();
        stunbullet.transform.SetParent(transform);
        stunbullet.gameObject.SetActive(false);
        _stunBullets.Add(stunbullet);

        return stunbullet;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = null;
        GameObject bulletObject = (GameObject)Resources.Load("Prefabs/Bullets/Bullet");
        bullet = Instantiate(bulletObject).GetComponent<Bullet>();
        bullet.transform.SetParent(transform);
        bullet.gameObject.SetActive(false);
        _bullets.Add(bullet);

        return bullet;
    }
}
