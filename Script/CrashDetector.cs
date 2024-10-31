using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float MagicMumber = 1f;
    [SerializeField] ParticleSystem CrashEffect;
    [SerializeField] AudioClip CrashSFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Invoke("ReLoadScense", MagicMumber);
            GetComponent<AudioSource>().PlayOneShot(CrashSFX);
            CrashEffect.Play();
        }
    }

    void ReLoadScense()
    {
        SceneManager.LoadScene(0);
    }    
}
