using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter{

    [SerializeField] private List<Sprite> enemySprites = new List<Sprite>();
    public float moveSpeed = 0f;
    private bool moveRight = true;
    public event Action<Enemy> EnemyDeath;
    public event Action OutOfBounds;

    private void Update() {
        if (moveRight) {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }
        else {
            transform.position += -1 * Vector3.right * Time.deltaTime * moveSpeed;
        }
    }

    public override void Die() {
        base.Die();
        EnemyDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void SetEnemySprite() {
        spriteRenderer.sprite = enemySprites[UnityEngine.Random.Range(0,enemySprites.Count)];
    }
    public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
    }

    public void FlipMovement() {
        moveRight = !moveRight;
    }
    public void HitBoundry() {
        OutOfBounds?.Invoke();
    }
}
