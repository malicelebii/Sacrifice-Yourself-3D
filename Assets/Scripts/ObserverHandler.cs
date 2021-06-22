using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverHandler : MonoBehaviour
{
    //Movement(oto)
    //Raycast ile kontrol
    public GameObject[] waypoints;
    public Animator observerAnimator;
    int current = 0;
    // public float rotationSpeed;
    public float speed;

    BombermanHandler bomberman;
    float WPradius = 1;
    public Vector3 direction;
    RaycastHit enemy;
    [SerializeField] Transform rayCastFirstPoint;
    [SerializeField] Transform rayCastSecondPoint;
    [SerializeField] Transform rayCastThirdPoint;

    [SerializeField] GameObject observer;
    Vector3 rayDir;
    public static bool isDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        observerAnimator.SetBool("isRun", true);
    }

    // Update is called once per frame
    void Update()
    {
        // if (rayCastFirstPoint.position.x > rayCastThirdPoint.position.x)
        // {
        for (float i = rayCastFirstPoint.position.x; i <= rayCastThirdPoint.position.x; i += 0.01f)
        {
            rayDir = new Vector3(rayCastFirstPoint.position.x - rayCastSecondPoint.position.x, transform.position.y, rayCastFirstPoint.position.z - rayCastSecondPoint.position.z);
            // Debug.Log(rayDir);

        }
        // }
        // else
        // {
        //     for (float i = rayCastFirstPoint.position.x; i <= rayCastThirdPoint.position.x; i -= 0.01f)
        //     {
        //         rayDir = new Vector3(rayCastSecondPoint.position.x+i, transform.position.y, transform.position.z);
        //         Debug.Log(rayDir);
        //         if (Physics.Raycast(rayCastSecondPoint.position, /*transform.forward*/ rayCastFirstPoint.position-rayCastSecondPoint.position, out enemy,5f))
        //         {
        //             if (enemy.collider.gameObject.tag == "Bomberman")
        //             {
        //                 isDetected = true;
        //                 Debug.Log("ışık temas etti");
        //                 observerAnimator.SetBool("isFired", true);
        //                 enemy.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //             }
        //         }
        //     }
        // }
        bomberman = GameObject.Find("stickman@Running").GetComponent<BombermanHandler>();
        if (isDetected == true || bomberman.bombing == true)
        {
            //    observerAnimator.SetBool("isBombed",true);
            //    observerAnimator.SetBool("isDeath",false);
            //    observerAnimator.SetBool("isRun",false);
            return;
        }
        //hareket.
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

        /* Switch direction */
        direction = (waypoints[current].transform.position - transform.position);
        if (direction != Vector3.zero)
        {
            // Quaternion toRatation = Quaternion.LookRotation(direction, Vector3.up);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRatation, rotationSpeed * Time.deltaTime);
            transform.forward = direction;
        }



        //dizi ile conic raycast yapabilirsin.
        for (float i = -0.5f; i < 1.3f; i += 0.1f)
        {
            /*degerler dinamik olmalı ki vector dogru olsun.yürüdüğü yönde x-z sürekli değisken bu da kosul gerekttiriyor.*/
            if (Physics.Raycast(transform.position, /*transform.forward*/ transform.forward + new Vector3(i, 0, 0), out enemy, 3.8f))
            {
                if (enemy.collider.gameObject.tag == "Bomberman")
                {
                    isDetected = true;
                    Debug.Log("ışık temas etti");
                    observerAnimator.SetBool("isFired", true);
                    enemy.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }

        }

    }




}