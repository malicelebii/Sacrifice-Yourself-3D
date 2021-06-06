using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUN : MonoBehaviour
{

    public float TaramaHizi;
    public GameObject MermiCikisNoktasi;
    public bool AtesEdebilir;
    float GunTimer;

    public ParticleSystem MuzzleFlash;
    AudioSource SesKaynak;
    public AudioClip AtesSesi;


    // Start is called before the first frame update
    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (  AtesEdebilir == true && Time.time > GunTimer || ObserverHandler.isDetected == true)//koşul eklenecek
        {
            Fire();

            MuzzleFlash.Play();
            GunTimer = Time.time + TaramaHizi;
            ;

        }
    }

    void Fire()
    {

        if (Physics.Raycast(MermiCikisNoktasi.transform.position, MermiCikisNoktasi.transform.forward))
        {
            MuzzleFlash.Play();
            SesKaynak.Play();
            SesKaynak.clip = AtesSesi;




        }
    }

}