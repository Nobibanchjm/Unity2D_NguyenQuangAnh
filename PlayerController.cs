 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float tocdobay = 10f;
    [SerializeField] float Tocdoxoay = 2f;
    [SerializeField] AudioClip mainsound;
    Rigidbody rgb;
    AudioSource AudioSource;

    //Hệ thống hạt Boost và đổi chiều
    [SerializeField] ParticleSystem BootsParticle;
    [SerializeField] ParticleSystem MoveRightParticle;
    [SerializeField] ParticleSystem MoveLeftParticle;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrush();
        processRatation();
    }


    void processThrush()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrush();
        }
        else
        {
            StopThrush();
        }
    }


    void StopThrush()
    {
        AudioSource.Stop();
        BootsParticle.Stop();
    }


    void StartThrush()
    {
        rgb.AddRelativeForce(Vector3.up * tocdobay * Time.deltaTime); // Vector3.up = (0,1,0)
        Debug.Log("Bạn đã ấn space");

        if (!AudioSource.isPlaying)
        {
            //AudioSource.Play();
            AudioSource.PlayOneShot(mainsound);
        }

        if (!BootsParticle.isPlaying)
        {
            BootsParticle.Play();
        }
    }


    void processRatation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            QuayRocket(Tocdoxoay);
            if (!MoveLeftParticle.isPlaying)
            {
                MoveLeftParticle.Play();
            }

        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            QuayRocket(-Tocdoxoay);
            if (!MoveRightParticle.isPlaying)
            {
                MoveRightParticle.Play();
            }

        }

        else 
        {
            MoveLeftParticle.Stop();
            MoveRightParticle.Stop();
        }
    }
    

    void QuayRocket(float Giatrixoay)
    {
        rgb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Giatrixoay * Time.deltaTime);
        rgb.freezeRotation = false;
    }    
}
