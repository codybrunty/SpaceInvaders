using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBullets : MonoBehaviour{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet Bullet)) {
            Bullet.gameObject.SetActive(false);
        }
    }

}
