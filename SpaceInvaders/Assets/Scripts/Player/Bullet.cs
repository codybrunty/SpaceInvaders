using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float speed = 1f;
    private int damage = 10;

    private void Update() {
        transform.position += (Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public void SetBulletInfo(int damage, float speed) {
        this.damage = damage;
        this.speed = speed;
    }

}
