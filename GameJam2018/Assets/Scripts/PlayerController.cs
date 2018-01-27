using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera PlayerCamera;


    private InteractableItem currentClickedItem;
    public float lastDepthDistance;

    void Awake()
    {
        if (PlayerCamera == null)
            PlayerCamera = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InteractableItem hit = RayCastFromCamera();
            if (hit != null)
            {
                hit.OnClickEnter(GetMouseUIPosition());
                currentClickedItem = hit;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (currentClickedItem != null)
            {
                currentClickedItem.OnClickExit(GetMouseUIPosition());
                currentClickedItem = null;
            }
        }
        else if (currentClickedItem != null)
            currentClickedItem.OnClickStay(GetMouseUIPosition());


    }

    private InteractableItem RayCastFromCamera()
    {

        RaycastHit hit;
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            InteractableItem hitItem = hitObject.GetComponentInParent<InteractableItem>();
            if (hitItem != null)
            {
                lastDepthDistance = Vector3.Distance(transform.position, hitItem.transform.position);
                return hitItem;
            }

        }
        return null;
    }

    private Vector3 GetMouseUIPosition()
    {
        RaycastHit hit;
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,100f, LayerMask.GetMask("PhysicalUI")))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = lastDepthDistance;

        return PlayerCamera.ScreenToWorldPoint(mousePos);
    }

    


}
