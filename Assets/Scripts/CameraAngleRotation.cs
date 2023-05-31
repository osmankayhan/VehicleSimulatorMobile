using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleRotation : MonoBehaviour
{
    private float rotationSpeed = 30f;

    void LateUpdate()
    {
        //Mouse X Movement
        CameraMovement();
    }

    void CameraMovement()
    {
        if (Input.touchCount > 0) // En az bir dokunma giriþi varsa devam eder
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunma giriþini alýr

            if (touch.phase == TouchPhase.Moved) // Dokunma hareketi gerçekleþiyorsa devam eder
            {
                float rotationY = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

                transform.Rotate(0f, -rotationY * rotationSpeed * Time.deltaTime, 0f);
            }
        }
    }
}