using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHandler : MonoBehaviour
{

    public Animator[] masumlar;
    public Animator[] suclular;
    public Animator[] gozculer;

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
        }

        if (collision.tag == "Guilty")
        {
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
            for (int i = 0; i < masumlar.Length; i++)
            {
                if (masumlar[i].name == collision.name)
                {
                    masum_sayisi++;
                    masumlar[i].SetBool("isBombed", true);
                }
            }
            Debug.Log("degdi innocent");
        }
        // Change Camera
      
        Invoke("PanelHandler",1.5f);
        Invoke("AnimationHandler",0.5f);
        // AnimationHandler();
    }

    void AnimationHandler(){
        //AnimationHandler ve PanelHandler'ın if orderları aynı olmalı. Birini değiştirince diğeri de değişmeli
        if (ObserverHandler.isDetected == true)
        {
            for (int i = 0; i < masumlar.Length; i++)
            {
                masumlar[i].SetBool("isShotDown", true);
            }
            for (int i = 0; i < suclular.Length; i++)
            {
                suclular[i].SetBool("isFire", true);
            }
        }
        else
        {
            if (suclu_sayisi != suclular.Length || masum_sayisi != 0)
            {
                for (int i = 0; i < masumlar.Length; i++)
                {
                    if (masumlar[i].GetBool("isBombed") == false){
                        masumlar[i].SetBool("isShotDown", true);
                    }
                }
                for (int i = 0; i < suclular.Length; i++)
                {
                    if (suclular[i].GetBool("isDeath") == false){
                        suclular[i].SetBool("isFire", true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < masumlar.Length; i++)
                {
                    masumlar[i].SetBool("isHappy", true);
                }
            }
        }
    }

    void PanelHandler(){
        //AnimationHandler ve PanelHandler'ın if orderları aynı olmalı. Birini değiştirince diğeri de değişmeli
        if (ObserverHandler.isDetected == true)
        {
            // Yakalanma case'i
            GameOverPanel.SetActive(true);
            SucceedPanel.SetActive(false);
        }
        else
        {
            if (suclu_sayisi != suclular.Length || masum_sayisi != 0)
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
    }
}
