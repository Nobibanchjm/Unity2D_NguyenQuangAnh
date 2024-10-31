using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 vitrihientai;
    [SerializeField] Vector3 MovermentVecctor;
    [SerializeField] [Range(0,1)] float MovermentFector;

    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        vitrihientai = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (period < Mathf.Epsilon) { return; }
        float chuki = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(chuki *tau);

        MovermentFector = (rawSinWave + 1f) / 2f;

        Vector3 offSet = MovermentVecctor * MovermentFector;
        transform.position = vitrihientai + offSet;
    }
}
