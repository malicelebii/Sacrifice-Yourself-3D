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
    public bool bombing = false;
    public Vector3 direction;

    float counter = 7f;
    [SerializeField] int bombCounter;
    bool isGameStarted = false;

    float vertical, horizontal;
    public int speed;
    public Joystick control;
    Vector3 oldPosition;
    Vector3 newPosition;

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
        if (vertical != 0 || horizontal != 0 && bombCounter > 0)
        {
            newPosition = transform.position;
            count.text = counter.ToString();
            transform.up = new Vector3(0, 0, 0);
            transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime, Space.World);
            counter -= Time.deltaTime;
            // if (counter <= 0)
            // {
            //     is_running = false;
            //     bombing = true;
            //     count.enabled = false;
            // }
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
        else if (bombCounter > 0 && newPosition != oldPosition)
        {
            Debug.Log(bombCounter);
            bombCounter--;
            // running.SetBool("isRunning",false);
            // running.SetBool("isWalking",true);
            // is_running = false;
            oldPosition = newPosition;
            // transform.Translate(new Vector3(0, 0, 0));

        }
        else if (bombCounter <= 0)
        {
            is_running = false;
        }
        #endregion

        // #region vector
        // if (vertical > 0 && horizontal > 0)
        // {
        //     Debug.Log("sa?? yukar??");

        //     transform.Rotate(new Vector3(0, horizontal * 90, 0));
        // }
        // else if (vertical > 0 && horizontal < 0)
        // {
        //     Debug.Log("Sol yukar??");
        //     transform.Rotate(new Vector3(0, 270 - (horizontal * 90), 0));
        // }
        // else if (vertical < 0 && horizontal < 0)
        // {
        //     Debug.Log("Sol a??a????");

        //     transform.Rotate(new Vector3(0, 180 - (horizontal * 90), 0));
        // }
        // else if (vertical < 0 && horizontal > 0)
        // {
        //     Debug.Log("sa?? a??a????");

        //     transform.Rotate(new Vector3(0, 90 + (horizontal * 90), 0));
        // }

        // #endregion

        // is_running=true;


    }

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
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
        // Debug.Log("cal??s??yor.");
    }

    void Running()
    {
        //Movement ko??ula g??re is_running true'ya ??ek

        // bomb.SetActive(false);
        // walking.SetBool("isWalking", is_walking);
        running.SetBool("isRunning", is_running);
    }
    void Walking()
    {
        //Movement ko??ula g??re is_running true'ya ??ek

        // bomb.SetActive(false);
        // walking.SetBool("isWalking", is_walking);
        walking.SetBool("isWalking", is_walking);
    }


    //Collider ko??ula g??re Bombing fonksiyonu ??al????t??r.
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




    //Collider ko??ula g??re Fired fonksiyonu ??al????t??r.
    void Fired()
    {
        bool fired = true;


        isFired.SetBool("isFired", fired);
    }

    void DecreaseBombCounter()
    {
        bombCounter--;
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Observer" || collision.tag == "Guilty")
        {
            Bomb();
        }

    }

}
