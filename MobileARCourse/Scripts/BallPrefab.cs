using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrefab : MonoBehaviour {

    [SerializeField] AudioClip ballHit;
    [SerializeField] AudioClip ballDie;
    [SerializeField] AudioSource ballSource;

    bool hasHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            ballSource.PlayOneShot(ballHit);
        }
        hasHit = true;
        if (collision.gameObject.name == "DeathPlane")
        {
            ballSource.PlayOneShot(ballDie);
            Destroy(this.gameObject, 3.10f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
    }

    void Start () {
        hasHit = false;
        Destroy(this.gameObject, 20.0f);
    }
}
