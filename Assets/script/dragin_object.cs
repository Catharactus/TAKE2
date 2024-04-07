using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragin_object : MonoBehaviour
{
    Vector3 mousePositionOffset;

    bool distanceIsCorrect;
    bool placeIsCorrect;

    Vector3 blockInitialPosition;

    public float returnTime;
    public float correctMinimalDistance;
    public float placingTime;

    GameObject[] cells;

    private GameObject closestCell;

    private void Start()
    {

        InitBlok();

    }

    public void InitBlok()
    {
        blockInitialPosition = transform.position;
        cells = GameObject.FindGameObjectsWithTag("plan");
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

        //check the closest tile to mouse position 
        closestCell = FindClosestCell();

        Debug.Log(Vector3.Distance(closestCell.transform.position, transform.position));

    }

    private void OnMouseUp()
    {
        //check the closest tile to mouse position 
        if(Vector3.Distance(closestCell.transform.position, transform.position) <= correctMinimalDistance)
        {
            distanceIsCorrect = true;
        }
        else
        {
            distanceIsCorrect= false;
        }

        //check if the tile is correct for the block
        placeIsCorrect = true;

        if(distanceIsCorrect && placeIsCorrect)
        {
            //place tile
            StartCoroutine(PlaceCell(closestCell));
        }
        else
        {
            //return tile to begin place 
            StartCoroutine(ReturnToInitialPos());
        }

    }

    private GameObject FindClosestCell()
    {
        float smallestDistance = 200f;

        GameObject currentClosestCell = null;

        foreach(GameObject cell in cells)
        {
            float currentDistance = Vector3.Distance(transform.position, cell.transform.position);

            if(currentDistance <= smallestDistance)
            {
                smallestDistance = currentDistance;

                currentClosestCell = cell;
            }
        }

        foreach(GameObject cell in cells )
        {
            Transform CellTransform = cell.transform;

            Transform cellCube = CellTransform.GetChild(0);

            Renderer cubeRenderer = cellCube.GetComponent<Renderer>();

            if (cell != currentClosestCell)
            {
                cubeRenderer.enabled = false;
            }
            else
            {
                cubeRenderer.enabled = true;
            }
        }
        
        return currentClosestCell;
         
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

    private IEnumerator PlaceCell(GameObject cell)
    {
        float elaspeTime = 0f;
        Vector3 currentBlockPos = transform.position;
        Vector3 newTransform = new Vector3(cell.transform.position.x, cell.transform.position.y, transform.position.z);

        while (elaspeTime < placingTime)
        {
            transform.position = Vector3.Lerp(currentBlockPos, newTransform, elaspeTime / placingTime);
            elaspeTime += Time.deltaTime;
            yield return null;  
        }

        transform.position = newTransform;
    }
}
