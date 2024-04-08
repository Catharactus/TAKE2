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
        
        blockInitialPosition = transform.position;
        
        //dragingOffset = transform.position - Placeur.transform.position;

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
            foreach(GameObject cell in cells)
            {
                if (Vector3.Distance(cell.transform.position, placeur.transform.position) <= correctMinimalDistance)
                {
                    placeurCheck = true;
                }
            }

            if(placeurCheck == true)
            {
                placeursCheck = false;
            }
        }

        Debug.Log(placeursCheck);

        if(placeursCheck)
        {
            //place tile
            //StartCoroutine(PlaceCell(closestCell));
        }
        else
        {
            //return tile to begin place 
            StartCoroutine(ReturnToInitialPos());
        }

    }

    private void FindClosestCell()
    {
        closestCell = new List<GameObject>();
        //iterate over each placeur
        foreach(GameObject placeur in placeurs)
        {
            float smallestDistance = 200f;

            //iterate each cell
            foreach(GameObject cell in cells)
            {  
                //compute distance 
                float currentDistance = Vector3.Distance(placeur.transform.position, cell.transform.position);

                if (currentDistance <= smallestDistance)
                {
                    smallestDistance = currentDistance;

                    closestCell.Add(cell);
                }
            }

        }
        Debug.Log(closestCell.Count);

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
