using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour{

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private int MaxHealth = 100;
    int health;

    private void Start() {
        ResetCharacterHealth();
    }

    public virtual void Die() {
        //Debug.Log(gameObject.name + " Died");
    }

    public void TakeDamage(int damage) {
        health = Mathf.Clamp(health-damage,0,int.MaxValue);
        if (health == 0) {
            Die();
        }
    }

    public Vector2 GetBounds() {
        return spriteRenderer.bounds.size;
    }

    public void ResetCharacterHealth() {
        health = MaxHealth;
    }

}
