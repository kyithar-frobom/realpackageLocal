using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class Ani_OrbitCamera1 : MonoBehaviour
{
    public Transform target;
    #if UNITY_ANDROID
    public float xSpeed = 7.0f;
    public float ySpeed = 7.0f;
    #else
    public float xSpeed = 0.5f;
    public float ySpeed = 0.5f;
    #endif
    public float smoothTime = 2f;
    public float autoTimer = 5f;

    float velocityX = 0.0f;
    float velocityY = 0.0f;
    bool faster;
    private bool rkeyActive;



    void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.rotation = Quaternion.Euler( 90, 0 , 0 );

        target.position = new Vector3(0, 0, 0);
        target.rotation = Quaternion.Euler( 0, 0, 0);
        // target2.position = new Vector3(0, 0, 0);
        // target2.rotation = Quaternion.Euler( 0, 0, 0);

    }


    void LateUpdate()
    {
        if (target != null)
        {
             //if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }

            Vector3 position = target.position;

            transform.RotateAround( position, Vector3.forward, -velocityX);
            transform.RotateAround( position, Vector3.left, -velocityY);

            Camera.main.transform.position = new Vector3(0, 15, 0);
            Camera.main.transform.rotation = Quaternion.Euler( 90, 0 , 0 );

            //target.position = new Vector3(0.001f, 0.131f, 0.031f);
            target.position = new Vector3(0, 0, 0);


            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
        else
        {
            Debug.LogWarning("Orbit Camera - No Target Set");
        }
    }


}