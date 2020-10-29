using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    private float panDetect = 15f;
    private float minHeight = 10f;
    private float maxHeight = 100f; // adjust as needed

    // Start is called before the first frame update; USE FOR INITIALIZATION!
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update() // NEVER PUT RAY CASTS IN THE UPDATE METHOD!
    {
        MoveCamera();
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation; //Resets the camera rotation should the player hit [Space]; change key later
        }
    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.LeftArrow) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed;
        }

        else if (Input.GetKey(KeyCode.RightArrow) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed;
        }

        else if (Input.GetKey(KeyCode.DownArrow) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed;
        }

        moveY += Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 20);

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

        Vector3 newPos = new Vector3(moveX, moveY, moveZ);


        Camera.main.transform.position = newPos;

    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if (Input.GetKey(KeyCode.LeftShift)) //change key later
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
        }

        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }
}
