using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShaderChooser : MonoBehaviour
{
    public static Dictionary<string, Shader> shaderDic = new Dictionary<string, Shader>()
    {
        { "Standard", Shader.Find("Standard")},
        //{ "Highlighted",Shader.Find("Legacy Shaders/Self-Illumin/Diffuse")}
        { "Highlighted", Shader.Find("Unlit/Texture") }
    };

    public static void SetShader(Collider col, string shaderName)
    {
        Renderer rend = col.gameObject.GetComponent<Renderer>();
        if (rend != null)
            rend.material.shader = shaderDic[shaderName];
    }
    public static void SetShader(Collider col, Shader shader)
    {
        Renderer rend = col.gameObject.GetComponent<Renderer>();
        if (rend != null)
            rend.material.shader = shader;
    }


    public static void SetShader(GameObject obj, string shaderName)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        List<Renderer> allRenderer = null;
        if (renderers.Length != 0)
        {
            allRenderer = new List<Renderer>(renderers);
            if (rend != null)
                allRenderer.Add(rend);
        }
        else if (rend != null)
            allRenderer = new List<Renderer>() { rend };

        if (allRenderer != null)
        {
            foreach(Renderer r in allRenderer)
            {
                r.material.shader = shaderDic[shaderName];
            }
        }
    }
    public static void SetShader(GameObject obj, Shader shader)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        List<Renderer> allRenderer = null;
        if (renderers.Length != 0)
        {
            allRenderer = new List<Renderer>(renderers);
            if (rend != null)
                allRenderer.Add(rend);
        }
        else if (rend != null)
            allRenderer = new List<Renderer>() { rend };

        if (allRenderer != null)
        {
            foreach (Renderer r in allRenderer)
            {
                r.material.shader = shader;
            }
        }
    }
}
