using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEnemiesOOB : MonoBehaviour{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.HitBoundry();
        }
    }

}
