using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Colliderhander : MonoBehaviour
{
    [SerializeField] float Thoigiancho = 1f;

    AudioSource audioSource;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Finish;

    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem FinishParticle;

    bool IsTransisioning = false; //đang chuyển đổi
    bool Vohieuhoa = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debugkey();
    }



    void Debugkey()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();  
        }

        else if (Input.GetKey(KeyCode.K)) 
        { 
            Vohieuhoa = !Vohieuhoa;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (IsTransisioning || Vohieuhoa) { return; } // Nếu đang trong quá trình chuyển đổi thì bỏ qua

        switch (other.gameObject.tag)
        {
            case "Start":
                UnityEngine.Debug.Log("Phi thuyền đang ở vạch đích");
                break;
            case "Finish":
                TimeNextLevel(); // Hoàn thành level và chuyển sang level tiếp theo
                break;
            case "Feul":
                UnityEngine.Debug.Log("Phi thuyền đã nạp nhiên liệu");
                break;
            default:
                StartCrashSeqenque(); // Nếu va chạm với đối tượng không xác định thì bắt đầu quá trình tàu bị phá hủy
                break;
        }
    }


    void StartCrashSeqenque()
    {
        IsTransisioning = true;
        DeathParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", Thoigiancho);
    }   
    

    void ReloadLevel()
    {
        int canhhientai = SceneManager.GetActiveScene().buildIndex; //Lấy scene hiện tại ví dụ đang ở màn 3 sẽ trở lại màn 3
        UnityEngine.Debug.Log("Xin lỗi, Phi thuyền đã nổ tung");
        SceneManager.LoadScene(canhhientai);
    }


    void TimeNextLevel()
    {
        IsTransisioning = true;
        FinishParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(Finish);
        GetComponent<PlayerController>().enabled = false;
        Invoke("NextLevel", Thoigiancho);
    }   
    

    void NextLevel()
    {
        int canhtieptheo = SceneManager.GetActiveScene().buildIndex + 1;
        if (canhtieptheo == SceneManager.sceneCountInBuildSettings) // nếu cảnh hiện tại đạt tối đa trong buidseting về level 1
        {
            canhtieptheo = 0;
        }
        SceneManager.LoadScene(canhtieptheo);
    }    

}
