using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour 
{
    public float offset = 1.0f;
    
	public Camera m_Camera = null;
	void Start()
	{
		if (m_Camera == null)
        	SetM_Camera();
    }

	// Update is called once per frame
	void Update () 
	{
        //transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
        //							m_Camera.transform.rotation * Vector3.up);
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, Vector3.up);
        //transform.position = (transform.parent.position + offset * transform.parent.up) + offset* Vector3.up;
    }

    public void SetM_Camera()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void SetM_Camera(Camera cam)
    {
        m_Camera = cam;
    }
}
