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

    public float returnTime;
    public float correctMinimalDistance;
    public float placingTime;
    public float timeToRotate = 0.2f;

    GameObject[] cells;

    List<GameObject> fullfamily = new List<GameObject>();

    private List<GameObject> closestCell = new List<GameObject>();
    private GameObject block;
    private List<GameObject> placeurs = new List<GameObject>();
    private GameObject firstPlaceur;

    private Vector3 blockOffset;

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

        bool firstPlaceurFound = false;
        
        //find placeurs 
        foreach(GameObject child in fullfamily)
        {
            if(child.tag == ("placeur"))
            {
                placeurs.Add(child);

                if(firstPlaceurFound == false)
                {
                    firstPlaceur = child;

                    firstPlaceurFound=true;
                }
            }
        }
        
        blockInitialPosition = transform.position;
        blockOffset = blockInitialPosition - firstPlaceur.transform.position;
        
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
        FindClosestCell();

        //Debug.Log(Vector3.Distance(closestCell.transform.position, Placeur.transform.position));

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RotateBlock());
        }

    }

    private void OnMouseUp()
    {
        bool placeursCheck = true;

        foreach(GameObject placeur in placeurs)
        {

            bool placeurCheck = false;

            foreach(GameObject cell in closestCell)
            {
                if (Vector3.Distance(cell.transform.position, placeur.transform.position) <= correctMinimalDistance)
                {
                    placeurCheck = true;
                    Debug.Log("placeur " + placeur + "at pos " + Vector3.Distance(cell.transform.position, placeur.transform.position));
                }

            }

            if(placeurCheck != true)
            {
                placeursCheck = false;
            }
        }

        Debug.Log(placeursCheck);

        if(placeursCheck)
        {
            //place tile
            StartCoroutine(PlaceCell(firstPlaceur));
        }
        else
        {
            //return tile to begin place 
            StartCoroutine(ReturnToInitialPos());
        }

        resetCells();

    }

    private void resetCells()
    {
        foreach(GameObject cell in cells)
        {
            Transform cubeShow = cell.transform.GetChild(0);

            Renderer cellRenderer = cubeShow.GetComponent<Renderer>();

            if (cellRenderer.enabled == true)
            {
                cellRenderer.enabled = false;
            }
        }
    }

    private void FindClosestCell()
    {
        closestCell = new List<GameObject>();
        //iterate over each placeur
        foreach(GameObject placeur in placeurs)
        {
            float smallestDistance = 200f;

            GameObject currentClosestCell = null;
            //iterate each cell
            foreach(GameObject cell in cells)
            {  
                //compute distance 
                float currentDistance = Vector3.Distance(placeur.transform.position, cell.transform.position);

                if (currentDistance <= smallestDistance)
                {
                    smallestDistance = currentDistance;
                    currentClosestCell = cell;

                }
            }

            closestCell.Add(currentClosestCell);
        }

        //Debug.Log(closestCell.Count);

        foreach(GameObject cell in cells)
        {
            Transform CellTransform = cell.transform;

            Transform cellCube = CellTransform.GetChild(0);

            Renderer cubeRenderer = cellCube.GetComponent<Renderer>();

            if (closestCell.Contains(cell))
            {
                cubeRenderer.enabled = true;
            }
            else
            {
                cubeRenderer.enabled = false;
            }
        }
         
    }

    private Vector3 findClosestCellButOne(GameObject placeur)
    {
        Vector3 closestCell = new Vector3();

        foreach(GameObject cell in cells)
        {
            if(Vector3.Distance(placeur.transform.position, cell.transform.position) <= correctMinimalDistance)
            {
                closestCell = cell.transform.position;
            }
        }

        return closestCell;
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
        Vector3 closestCell = findClosestCellButOne(firstPlaceur);
        Vector3 NewTransform = new Vector3(closestCell.x + blockOffset.x, closestCell.y + blockOffset.y, transform.position.z);

        while (elaspeTime < placingTime)
        {
            transform.position = Vector3.Lerp(currentBlockPos, NewTransform, elaspeTime / placingTime);
            elaspeTime += Time.deltaTime;
            yield return null;  
        }

        transform.position = NewTransform;
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
