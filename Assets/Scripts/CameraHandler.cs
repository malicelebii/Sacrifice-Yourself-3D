using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject Tpsmode;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position - Tpsmode.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = Tpsmode.transform.position + offset;
    }
}
