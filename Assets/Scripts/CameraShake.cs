using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
    private float shakedur = 1f;
    private readonly float shakeamount = 0.4f;
    private readonly float decreaseFactor = 2f;
    private Vector3 originposition;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        originposition = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakedur > 0)
        {
            camTransform.localPosition = originposition + Random.insideUnitSphere * shakeamount;
            shakedur -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakedur = 0;
            camTransform.localPosition = originposition;
        }
    }
}