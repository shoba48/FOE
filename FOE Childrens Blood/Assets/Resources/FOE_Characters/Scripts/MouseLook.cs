using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
    Transform player;
    Rigidbody rb;
    Vector3 defaultPos;

    void LookAround()
    {
        // get current localEulerAngles (lEA) and adjust some stupit rotation around Z-Axis; 
        Vector3 lEA = transform.localEulerAngles;
        lEA.z = 0;
        transform.localEulerAngles = lEA;
        // get Mouse movement and limit the angle 
        float h = Input.GetAxis("Mouse X");
        float v = -Input.GetAxis("Mouse Y"); // inverted
        if (lEA.y + h > 90 && lEA.y + h < 270) transform.localEulerAngles = lEA;
        else lEA.y += h;
        if (lEA.x + v > 80 && lEA.x + v < 270) transform.localEulerAngles = lEA;
        else lEA.x += v;
        // apply rotation
        transform.localEulerAngles = lEA;
        // rotate camera to parents forward when parent is moving 
        if (rb.velocity.magnitude > 0.1f)
        {
            lEA = transform.localEulerAngles;
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, 3 * Time.deltaTime);
            transform.localEulerAngles = new Vector3(lEA.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

    }
    void ChangeDistance()
    {
        Vector3 lEA = transform.localEulerAngles;
        float deltaDistance = Input.mouseScrollDelta.y * Time.deltaTime * 10;
        float angle = 0;
        if (transform.localPosition.z + deltaDistance < defaultPos.z)
        {
            transform.localPosition += new Vector3(0, -deltaDistance, 2.5f * deltaDistance);
            if (deltaDistance != 0)
            { 
                angle = Mathf.Cos((defaultPos.z - 1 / (defaultPos - transform.localPosition).magnitude - transform.localPosition.z) / (defaultPos - transform.localPosition).magnitude);
                if (lEA.x < 270) //should be between 80 and 0
                {
                    lEA.x -= 0.5f * (lEA.x - angle);
                    transform.localEulerAngles = lEA;
                }
                else // should be between 270 and 360
                {
                    lEA.x -= 0.5f * (lEA.x - (360 + angle));
                    transform.localEulerAngles = lEA;
                }
            }
        }
        if (defaultPos.z - transform.localPosition.z < -0.5f)
        {
            lEA.x -= 0.1f * (lEA.x);
            transform.localEulerAngles = lEA;
        }



    }

	public void ReStart()
	{
		Start();
	}

    // Use this for initialization
    void Start ()
    {
        player = GetComponentInParent<Attributes>().gameObject.transform;
        rb = GetComponentInParent<Rigidbody>();
		//defaultPos = transform.parent.gameObject.GetComponentInChildren<Head>().transform.position;
		defaultPos = new Vector3(0, 1.5f, 0.2f);
        Cursor.lockState = CursorLockMode.Confined;
    }
	
    

	// Update is called once per frame
	void Update ()
    {
        if (this.gameObject.tag == "MainCamera")
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.Mouse0)) Cursor.lockState = CursorLockMode.Confined;
            if (Cursor.lockState == CursorLockMode.Confined)
            {
                LookAround();
                ChangeDistance();
            }
        }
    }
}
