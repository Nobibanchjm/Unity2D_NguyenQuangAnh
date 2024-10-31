using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] float MagicMunber = 1f;
    [SerializeField] ParticleSystem FinishEffect; //particlesystem cho phép tạo ra và điều khiển các hiệu ứng hạt như lửa, nước, khói, tia sáng...
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            FinishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReLoadScene", MagicMunber);
        }    
    }
    
    void ReLoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
