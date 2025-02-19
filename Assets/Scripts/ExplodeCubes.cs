using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            for (int i = collision.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = collision.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(500f, Vector3.up, 50f);
                child.SetParent(null);
            }
            Camera.main.gameObject.AddComponent<CameraShake>();
            GameObject VFX = Instantiate(explosion, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(VFX, 2.5f);
            Destroy(collision.gameObject);
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}