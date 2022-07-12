using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter{


    [Header("Shooting Attributes")]
    public float fireWaitTime = .25f;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;

    private Coroutine shootingCo;


    public event Action PlayerDeath;

    public void StartPlayer() {
        spriteRenderer.enabled = true;
        StartShooting();
    }

    public void StartShooting() {
        shootingCo=StartCoroutine(AutoFire());
    }

    public void StopShooting() {
        if (shootingCo != null) { 
            StopCoroutine(shootingCo);
        }
    }

    IEnumerator AutoFire() {
        yield return new WaitForSeconds(fireWaitTime);
        GameObject bulletGO = ObjectPoolManager.instance.SpawnFromPool("Bullet", transform.position,Quaternion.identity);
        bulletGO.GetComponent<Bullet>().SetBulletInfo(bulletDamage, bulletSpeed);
        shootingCo=StartCoroutine(AutoFire());
    }

    [ContextMenu("test death")]
    public override void Die() {
        base.Die();
        PlayerDeath?.Invoke();
        spriteRenderer.enabled = false;
    }

    public void ResetPlayer() {
        ResetCharacterHealth();
        StopShooting();
    }

}
