using UnityEngine;
using System.Collections;

public class DoorTurner : ObjectToUse
{
    public float turnSpeed;
    public float openAngle = 75.0f;
    private bool open = false;



	public override void DoAction(Transform somebody)
    {
        ToogleDoor();
    }



    public void ToogleDoor()
    {
        if (open)
        {
            open = false;
            transform.parent.localEulerAngles = new Vector3(transform.parent.localEulerAngles.x, transform.parent.localEulerAngles.y - openAngle, transform.parent.localEulerAngles.z);
        }
        else
        {
            open = true;
			transform.parent.localEulerAngles = new Vector3(transform.parent.localEulerAngles.x, transform.parent.localEulerAngles.y + openAngle, transform.parent.localEulerAngles.z);
        }
    }




	
	
}
