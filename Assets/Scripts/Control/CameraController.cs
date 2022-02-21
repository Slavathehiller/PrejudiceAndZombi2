using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject gameCamera;
    private int cameraScrollSpeed = 10;
    private int cameraRotateSpeed = 50;
    void Start()
    {
        gameCamera = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion ang = gameCamera.transform.rotation;
        
        if (Input.GetKey(KeyCode.E))
        {
            ang.eulerAngles = new Vector3(ang.eulerAngles.x, transform.eulerAngles.y + cameraRotateSpeed * Time.deltaTime, ang.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            ang.eulerAngles = new Vector3(ang.eulerAngles.x, transform.eulerAngles.y - cameraRotateSpeed * Time.deltaTime, ang.eulerAngles.z);
        }

        gameCamera.transform.rotation = ang;

        Vector3 forward = gameCamera.transform.TransformDirection(Vector3.forward);
        if (Input.GetKey(KeyCode.A)){
            gameCamera.transform.Translate(Vector3.back * cameraScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameCamera.transform.Translate(Vector3.forward * cameraScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameCamera.transform.Translate(Vector3.left * cameraScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameCamera.transform.Translate(Vector3.right * cameraScrollSpeed * Time.deltaTime);
        }            
    }
}
