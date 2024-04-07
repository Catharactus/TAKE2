using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragin_object : MonoBehaviour
{
    Vector3 mousePositionOffset;

    bool placeIsCorrect;

    Vector3 blockInitialPosition;

    public float returnTime;

    private void Start()
    {
        InitBlok();
    }

    public void InitBlok()
    {
        blockInitialPosition = transform.position;
    }
    
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

    private void OnMouseDrag()
    {
        // Update the object's position based on the mouse movement
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp()
    {
        //check the closest tile to mouse position 

        //check if the tile is correct for the block

        if(placeIsCorrect)
        {
            //place tile
        }
        else
        {
            //return tile to begin place 
            StartCoroutine(ReturnToInitialPos());
        }

    }

    private IEnumerator ReturnToInitialPos()
    {

        float elaspeTime = 0f;
        Vector3 currentBlockPos = transform.position;

        while (elaspeTime < returnTime)
        {
            transform.position = Vector3.Lerp(currentBlockPos, blockInitialPosition, elaspeTime / returnTime);
            elaspeTime += Time.deltaTime;
            yield return null;
        }

        transform.position = blockInitialPosition;
    }
}
