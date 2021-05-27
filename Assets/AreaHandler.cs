using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHandler : MonoBehaviour
{

    public Animator[] masumlar;
    public Animator[] suclular;
    public Animator[] gozculer;

    public Camera death_camera;
    public Camera real_camera;

    public int gozcu_sayisi;
    public int masum_sayisi;
    public int suclu_sayisi;
    public GameObject GameOverPanel;
    public GameObject SucceedPanel;
    public GameObject JoystickCanvas;
    // public Animator masum_death;
    // public Animator suclu_death;
    // public Animator observer_death;
    // public Animator suclu_death2;
    // Start is called before the first frame update
    void Start()
    {
        gozcu_sayisi = 0;
        masum_sayisi = 0;
        suclu_sayisi = 0;
        real_camera.enabled = true;
     death_camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Observer")
        {
            for (int i = 0; i < gozculer.Length; i++)
            {
                if (gozculer[i].name == collision.name)
                {

                    gozculer[i].SetBool("isDeath", true);
                    gozcu_sayisi++;
                }
            }
            // asd[0].SetBool("isDeath", true);
            Debug.Log("degdi");
        }

        if (collision.tag == "Guilty")
        {
            // asd[2].SetBool("isDeath", true);
            // asd[3].SetBool("isDeath", true);
            // asd[5].SetBool("isDeath", true);
            for (int i = 0; i < suclular.Length; i++)
            {
                if (suclular[i].name == collision.name)
                {
                    suclu_sayisi++;
                    suclular[i].SetBool("isDeath", true);
                }

            }
            Debug.Log("degdi guilty");
        }

        if (collision.tag == "Innocent")
        {
            // asd[1].SetBool("isDeath", true);
            for (int i = 0; i < masumlar.Length; i++)
            {
                if (masumlar[i].name == collision.name)
                {
                    masum_sayisi++;
                    masumlar[i].SetBool("isDeath", true);
                }
            }
            Debug.Log("degdi innocent");
            Debug.Log(collision.name);
        }
        if (gozcu_sayisi + suclu_sayisi < masum_sayisi)
        {
            GameOverPanel.SetActive(true);
            SucceedPanel.SetActive(false);

        }
        else
        {
            if (ObserverHandler.isDetected == true)
            {
                GameOverPanel.SetActive(true);
                SucceedPanel.SetActive(false);
            }
            else
            {

                SucceedPanel.SetActive(true);
                GameOverPanel.SetActive(false);
            }

        }

        death_camera.enabled=true;
        real_camera.enabled=false;
        

    }
}
