using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleports : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.transform.position = new Vector3(collision.transform.position.x * -1, collision.transform.position.y, collision.transform.position.z);
        }
    }
}
