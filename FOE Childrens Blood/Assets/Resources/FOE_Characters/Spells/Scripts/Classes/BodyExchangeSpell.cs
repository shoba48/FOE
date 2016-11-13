using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class BodyExchangeSpell : Spell

{
    public GameObject mainCamera;
    private Vector3 mainCameraPosDefault;
    private Quaternion mainCameraRotDefault;
	Transform victimHead;
	Transform creatorHead;

    private bool success = false;

    public override void Awake()
    {
        base.Awake();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");//.GetComponent<Camera>().gameObject;
        mainCameraPosDefault = mainCamera.transform.localPosition;
        mainCameraRotDefault = mainCamera.transform.localRotation;
        LightShafts beam = GetComponent<LightShafts>();
        beam.m_Cameras = mainCamera.GetComponents<Camera>();
        //transform.localPosition = new Vector3(0, 1.5f, 0.2f);
    }

    void Start()
    {
        if (victim != null)
        {
            if (victim.GetComponent<ThirdPersonCharacter>() == null)
                victim = null;
			else{
			
				victimHead = victim.GetComponentInChildren<Head>().transform;
				transform.LookAt(victimHead);
			}
            
        }
		//transform.localPosition = new Vector3(0, 1.5f, 0);
		Head Head = transform.parent.gameObject.GetComponentInChildren<Head>();
		if (Head != null)
		{
			creatorHead = Head.transform;
			transform.parent = creatorHead.transform;
			transform.localPosition = Vector3.zero;
			transform.rotation = Quaternion.LookRotation(-transform.parent.up);
		}
    }
    public override void Update()
    {
        base.Update();

        if (victim != null)
            BodyExchange();
        else Invoke("EndSpell",1);
    }

    public override void EndSpell()
    {
        if (success == false && victim != null)
        {
			mainCamera.transform.localPosition = mainCameraPosDefault;
            mainCamera.transform.localRotation = mainCameraRotDefault;
        }
        
        GameObject.Destroy(this.gameObject);
    }
    
    
  
    void BodyExchange()
    {
        Vector3 direction;
        if (creator.tag == "Player")
        {
			direction = -(mainCamera.transform.position - victimHead.position);
            if (direction.magnitude > 0.1f)
                mainCamera.transform.position += direction.normalized * Time.deltaTime * (direction.magnitude + 1);
            else
            {                           //playerOff
                SwitchControllComponents(creator, victim);
                success = true;
                EndSpell();
            }
        }
        else if (victim.tag == "Player")
        {
			direction = -(mainCamera.transform.position - creatorHead.position );
            if (direction.magnitude > 0.1f)
                mainCamera.transform.position += direction.normalized * Time.deltaTime * (direction.magnitude + 1);
            else
            {                           //playerOff,otherOn
                SwitchControllComponents(victim, creator);
                success = true;
                EndSpell();
            }
        }
    }



    void SwitchControllComponents(GameObject toOff, GameObject toOn)
    {   // PLAYER_CREATION //
        toOn.tag = "Player";
        // enable all controll stuff 
        toOn.GetComponent<ThirdPersonUserControl>().enabled = true;
		toOn.GetComponent<ThirdPersonCharacter>().enabled = true;
		toOn.GetComponent<VisorSystem>().enabled = true;
		toOn.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		ShaderChooser.SetShader(toOn, toOn.GetComponent<ObjectToUse>().defaultShader);
        //toOn.GetComponent<ThirdPersonControl>().enabled = false;
        // enable Camera and tag it MainCamera
		mainCamera.transform.parent = toOn.transform;
		mainCamera.transform.localPosition = mainCameraPosDefault;
		mainCamera.transform.localRotation = mainCameraRotDefault;
		mainCamera.GetComponent<MouseLook>().ReStart();
        
        // NON_PLAYER_CREATION //
        toOff.tag = "Untagged";
        // disenable all controll stuff
        toOff.GetComponent<ThirdPersonUserControl>().enabled = false;
		toOff.GetComponent<ThirdPersonCharacter>().enabled = false;
		toOff.GetComponent<VisorSystem>().enabled = false;
		toOff.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
        //toOff.GetComponent<ThirdPersonControl>().enabled = true;
        // disenable Camera und untag it
 
        // make the Name and Healthbar Canvas proper
        //toOff.GetComponentInChildren<CameraFacingBillboard>().SetM_Camera(newCam);
    }
}
