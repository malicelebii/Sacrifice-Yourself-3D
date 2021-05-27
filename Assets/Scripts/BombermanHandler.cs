using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombermanHandler : MonoBehaviour
{
    public static bool isGameFinished = false;

    public Animator running;
    public Animator bombed;
    public Animator isFired;
    public Animator walking;
    // public Animator masum_death;
    // public Animator suclu_death;
    public Rigidbody rb;
    public GameObject bomb;
    // public GameObject masum;
    public GameObject area;
    public GameObject JoystickCanvas;
   
    // public GameObject suclu;

    public Text count;
    //MOVEMENT-ANALOG
    bool is_running;
    bool is_walking;
    bool bombing = false;
    public Vector3 direction;

    float counter = 7f;
    bool isGameStarted = false;

    float vertical, horizontal;
    public int speed;
    public Joystick control;

    private void FixedUpdate()
    {
        JoystickMove();
    }










    void JoystickMove()
    {
        if (bombing == true)
        {
            return;
        }

        #region move
        vertical = control.Vertical;
        horizontal = control.Horizontal;
        if (vertical != 0 || horizontal != 0)
        {
            // Debug.Log(vertical.ToString() + "veritcal");
            // Debug.Log(horizontal.ToString() + "horizontal");
            count.text = counter.ToString();
            transform.up = new Vector3(0, 0, 0);
            transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime, Space.World);
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                is_running = false;
                bombing = true;
                count.enabled = false;
            }
            direction = new Vector3(horizontal, 0, vertical);
            transform.forward = direction;
            is_running = true;
            // if(vertical>0.5 || horizontal >0.5){
            // is_running = true;
            // is_walking=false;
            // }
            // else{
            //     is_walking=true;
            //     is_running=false;
            // }
            isGameStarted = true;
        }
        else
        {
            is_running = false;
            transform.Translate(new Vector3(0, 0, 0));

        }
        #endregion

        // #region vector
        // if (vertical > 0 && horizontal > 0)
        // {
        //     Debug.Log("sağ yukarı");

        //     transform.Rotate(new Vector3(0, horizontal * 90, 0));
        // }
        // else if (vertical > 0 && horizontal < 0)
        // {
        //     Debug.Log("Sol yukarı");
        //     transform.Rotate(new Vector3(0, 270 - (horizontal * 90), 0));
        // }
        // else if (vertical < 0 && horizontal < 0)
        // {
        //     Debug.Log("Sol aşağı");

        //     transform.Rotate(new Vector3(0, 180 - (horizontal * 90), 0));
        // }
        // else if (vertical < 0 && horizontal > 0)
        // {
        //     Debug.Log("sağ aşağı");

        //     transform.Rotate(new Vector3(0, 90 + (horizontal * 90), 0));
        // }

        // #endregion

        // is_running=true;


    }

    // Start is called before the first frame update
    void Start()
    {
        // GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted == false)
        {
            return;
        }
        // if (bombing == true)
        // {
        //     if (Vector3.Distance(masum.transform.position, area.transform.position) < 5)
        //     {

        //         // Destroy(masum);
        //         //death
        //         masum_death.SetBool("isDeath",true);
        //         suclu_death.SetBool("isDeath",true);
        //     }
        // }
        if (bombing == true)
        {
            area.GetComponent<CapsuleCollider>().enabled = true;
            // GameOverPanel.SetActive(true);
            JoystickCanvas.SetActive(false);
        }

        Walking();
        Running();
        Bomb();
        // Fired();
        // Debug.Log("calısıyor.");
    }

    void Running()
    {
        //Movement koşula göre is_running true'ya çek

        // bomb.SetActive(false);
        // walking.SetBool("isWalking", is_walking);
        running.SetBool("isRunning", is_running);
    }
    void Walking()
    {
        //Movement koşula göre is_running true'ya çek

        // bomb.SetActive(false);
        // walking.SetBool("isWalking", is_walking);
        walking.SetBool("isWalking", is_walking);
    }


    //Collider koşula göre Bombing fonksiyonu çalıştır.
    void Bomb()
    {
        if (is_running == false || ObserverHandler.isDetected == true)
        {
            bombing = true;
            bomb.SetActive(true);
        }

        // walking.SetBool("isWalking", is_walking);
        bombed.SetBool("isBombed", bombing);
    }




    //Collider koşula göre Fired fonksiyonu çalıştır.
    void Fired()
    {
        bool fired = true;


        isFired.SetBool("isFired", fired);
    }



    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Observer" || collision.tag == "Guilty")
        {
            Debug.Log("degdi");
            Fired();
        }

    }

}
