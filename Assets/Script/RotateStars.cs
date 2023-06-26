using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStars : MonoBehaviour
{
    public Camera mainCamera;
    public float rotateSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = mainCamera.transform.position;
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true){
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true){
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true){
            transform.Rotate(-Vector3.right * rotateSpeed * Time.deltaTime, Space.World);
        }
        
        if (Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true)
        {
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}
