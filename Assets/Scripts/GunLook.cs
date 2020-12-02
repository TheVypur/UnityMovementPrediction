using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLook : MonoBehaviour
{
    Transform tCam;
    // Start is called before the first frame update
    void Start()
    {
        tCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(tCam.transform.forward * 40);
    }
}
