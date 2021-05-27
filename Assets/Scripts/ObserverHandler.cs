using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverHandler : MonoBehaviour
{
    //Movement(oto)
    //Raycast ile kontrol
    public GameObject[] waypoints;
    public Animator fire;
    int current = 0;
    // public float rotationSpeed;
    public float speed;
    float WPradius = 1;
    public Vector3 direction;
    RaycastHit enemy;

    public static bool isDetected=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(isDetected==true){
           return;
       }
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius) {
            current++;
            if (current >= waypoints.Length) {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

        /* Switch direction */
        direction = (waypoints[current].transform.position-transform.position);
        if (direction != Vector3.zero){
            // Quaternion toRatation = Quaternion.LookRotation(direction, Vector3.up);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRatation, rotationSpeed * Time.deltaTime);
            transform.forward = direction;
        }

        if (Physics.Raycast(transform.position, transform.forward, out enemy, 8.0f))
        {
            if (enemy.collider.gameObject.tag == "Bomberman")
            {
                isDetected=true;
                Debug.Log("ışık temas etti");
                fire.SetBool("isFired",true);
                enemy.collider.gameObject.GetComponent<MeshRenderer>().enabled=false;
            }
        }
    }
}