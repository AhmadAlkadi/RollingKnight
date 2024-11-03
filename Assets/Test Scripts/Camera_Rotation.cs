using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour
{
    float rotate = 0.0f;
    float nextRotationStop = 0.0f;
    bool keyPressE = false;
    bool keyPressQ = false;
    bool isRotationComplete = true;

    public float rotateSpeed = 1.0f;
    public float rotateStop = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && (keyPressE == false) && isRotationComplete)
        {
            isRotationComplete = false;
            keyPressE = true;
            nextRotationStop = rotate + rotateStop;
        }

        if (Input.GetKey(KeyCode.Q) && (keyPressQ == false) && (keyPressE != true) && isRotationComplete)
        {
            isRotationComplete = false;
            keyPressQ = true;
            nextRotationStop = rotate - rotateStop;
        }

        if(keyPressE == true)
        { 
            if (rotate <= nextRotationStop)
            {
                rotate += rotateSpeed;
                transform.localRotation = Quaternion.Euler(0, rotate, 0);
            }

            if (rotate >= nextRotationStop)
            {
                isRotationComplete = true;
                rotate = nextRotationStop;
                keyPressE = false;
            }
        }

        if (keyPressQ == true)
        {
            if (rotate >= nextRotationStop)
            {
                rotate -= rotateSpeed;
                transform.localRotation = Quaternion.Euler(0, rotate, 0);
            }

            if (rotate <= nextRotationStop)
            {
                isRotationComplete = true;
                rotate = nextRotationStop;
                keyPressQ = false;
            }
        }
    }
}
