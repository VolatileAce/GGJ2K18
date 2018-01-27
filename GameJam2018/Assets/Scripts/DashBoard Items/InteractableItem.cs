using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class InteractableItem : MonoBehaviour {

    public Material ButtonLightMat;
    public Color BaseColor;
    public Color ClickedColor;

    protected Material LightMatInstance;

    public MidiChannel Channel;

	public virtual void OnClickEnter(Vector3 inWorldMousePos)
    {
        LightMatInstance.color = ClickedColor;
    }

    public virtual void OnClickExit(Vector3 inWorldMousePos)
    {
        LightMatInstance.color = BaseColor;
    }

    public virtual void OnClickStay(Vector3 inWorldMousePos)
    {
        
    }


    protected virtual void Start()
    {
        LightMatInstance = GetInstanceMaterial(ButtonLightMat);
        LightMatInstance.color = BaseColor;

    }

    protected Material GetInstanceMaterial(Material in_Material)
    {

         
        foreach(InteractableItem item in gameObject.GetComponents<InteractableItem>())
        {
            if (item.LightMatInstance != null)
                return item.LightMatInstance;
        }


        Renderer[] AllRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in AllRenderers)
        {
            for (int i = 0; i < rend.sharedMaterials.Length; i++)
            {
                if (rend.sharedMaterials[i].Equals(in_Material))
                {
                    return rend.materials[i];
                }
            }

        }
        return null;
    }


    protected Vector3 ProjectPointOnPlane(Vector3 in_Point)
    {
        Vector3 planeNormal = transform.up.normalized;
        var distance = -Vector3.Dot(planeNormal.normalized, (in_Point - transform.position));
        return in_Point + planeNormal * distance;
    }

}
