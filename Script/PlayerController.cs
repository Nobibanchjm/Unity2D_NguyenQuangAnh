using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    [SerializeField] float Torque = 1f;
    [SerializeField] float bootspeed = 30f;
    [SerializeField] float normalspeed = 20f;
    public float acceleration = 5f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Controllerplayer();
        RespondToBoots();

    }


    void Controllerplayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(Torque); //AddTorque là phương thức được sử dụng đẻ thêm mô-men xoán vào một RightBody
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-Torque);
        }
    }

    void RespondToBoots()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(surfaceEffector2D.speed < bootspeed)
            {
                surfaceEffector2D.speed += acceleration * Time.deltaTime;
            }
            else
            {
                surfaceEffector2D.speed = bootspeed;
            }

        }
        else 
        {
            surfaceEffector2D.speed = normalspeed;
        }
    }
}
