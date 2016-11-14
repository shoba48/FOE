using UnityEngine;
using System.Collections;
using Fungus;
public class ObjectToUse : MonoBehaviour
{
    public float useRange = 3;
    public float autoActionRange = 10;

	public Flowchart flowchart;


    public Shader defaultShader;



	public virtual void DoAction(Transform somebody)
    {
        flowchart.SendFungusMessage("Use");
    }



    public virtual void AutoAction(Transform somebody)
    {
        //flowchartStoryLine.SendFungusMessage(this.gameObject.name);

    }



    // Use this for initialization
    public virtual void Start()
    {
        flowchart = transform.Find("Flowchart").gameObject.GetComponent<Flowchart>();

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            defaultShader = renderer.material.shader;
        else
        {
            renderer = GetComponentInChildren<Renderer>();
            if (renderer != null)
                defaultShader = renderer.material.shader;
            else
                defaultShader = Shader.Find("Standard");
        }
    }



    // Update is called once per frame
    public virtual void Update() { }



}
