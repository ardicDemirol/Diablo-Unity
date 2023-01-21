using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{

    public Texture2D cursorTexture;

    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    public GameObject mousePoint;

    void Update()
    {
        Cursor.SetCursor(cursorTexture,hotSpot,mode);

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 Lastpos = hit.point;
                    Lastpos.y = 0.35f;

                    Instantiate(mousePoint, Lastpos,Quaternion.identity);
                }
            }
        }
    }
}
