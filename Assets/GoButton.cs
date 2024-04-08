using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{

    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
        // Capture the mouse position and convert it to world coordinates
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        // Calculate the offset between the object's position and the mouse position
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

}
