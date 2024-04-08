using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragin_object : MonoBehaviour
{
    Vector3 mousePositionOffset;

    bool distanceIsCorrect;
    bool placeIsCorrect;

    Vector3 blockInitialPosition;
    Vector3 dragingOffset;

    public GameObject Placeur;

    public float returnTime;
    public float correctMinimalDistance;
    public float placingTime;
    public float timeToRotate = 0.2f;

    GameObject[] cells;

    List<GameObject> fullfamily = new List<GameObject>();

    private GameObject closestCell;
    private GameObject block;
    private List<GameObject> placeurs = new List<GameObject>();

    private void explorefamily(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Process the child GameObject (e.g., add it to a list)
            fullfamily.Add(child.gameObject);

            // Recurse into nested children
            explorefamily(child);
        }
    }
 
    public void InitBlok()
    {
        cells = GameObject.FindGameObjectsWithTag("plan");
        
        explorefamily(gameObject.transform);
        
        //find placeurs 
        foreach(GameObject child in fullfamily)
        {
            if(child.tag == ("placeur"))
            {
                placeurs.Add(child);
            }
        }
        
        Debug.Log(placeurs.Count);
        //dragingOffset = transform.position - Placeur.transform.position;
        //blockInitialPosition = transform.position;


        //get the block in the variable
        //Transform baseFinder = transform;
        //Transform placeur = baseFinder.transform.GetChild(0);
        //Transform blockTransform = placeur.transform.GetChild(0);

        //block = blockTransform.gameObject;

        //Debug.Log(block);
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

        //Debug.Log(Vector3.Distance(closestCell.transform.position, Placeur.transform.position));

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RotateBlock());
        }

    }

    private void OnMouseUp()
    {
        //check the closest tile to mouse position 
        if(Vector3.Distance(closestCell.transform.position, Placeur.transform.position) <= correctMinimalDistance)
        {
            distanceIsCorrect = true;
        }
        else
        {
            distanceIsCorrect= false;
        }

        //check if the tile is correct for the block
        placeIsCorrect = isPlaceCorrect();

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
            float currentDistance = Vector3.Distance(Placeur.transform.position, cell.transform.position);

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
        Vector3 newNewTransform = new Vector3(newTransform.x + dragingOffset.x, newTransform.y+dragingOffset.y, newTransform.z);

        while (elaspeTime < placingTime)
        {
            transform.position = Vector3.Lerp(currentBlockPos, newNewTransform, elaspeTime / placingTime);
            elaspeTime += Time.deltaTime;
            yield return null;  
        }

        transform.position = newNewTransform;
    }

    private bool isPlaceCorrect()
    {
        return true;
    }

    private IEnumerator RotateBlock()
    {
        float elaspeTime = 0f;
        Transform blockTransform = block.transform;
        Vector3 blockPosition = block.GetComponent<Renderer>().bounds.center;
        Quaternion rotatedBlock = Quaternion.Euler(blockTransform.rotation.eulerAngles + new Vector3(0f, 0f, 90f));


        while (elaspeTime < timeToRotate)
        {
            blockTransform.rotation = Quaternion.Lerp(blockTransform.rotation, rotatedBlock, elaspeTime / timeToRotate);
            elaspeTime += Time.deltaTime;
            yield return null;
        }
    }
}
