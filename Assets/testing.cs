using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public Transform tf;
    public GameObject wallObject;
    private wallScript wall;
    private Grid grid;
    private bool isBuilding;
    private GameObject tempObject;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(3,2,3,3f, new Vector3(-5,0,-8));
        isBuilding = false;
        wall = new wallScript();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.P))
        {   
            
            if(isBuilding == false)
            {
                
                tempObject= Instantiate(wallObject,new Vector3(-5f,-5f,-5f),Quaternion.identity);
                wall = tempObject.GetComponent<wallScript>();
                Debug.Log(tempObject.transform.name);
                isBuilding = true;
            }
            else if(isBuilding == true)
            {
                Destroy(tempObject);
                isBuilding = false;
                wall = null;
            }
            
        }
       
        
        if(isBuilding)
        {
            //Rotate object
            
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
                if(Input.GetKeyDown(KeyCode.K) && tempObject!=null)
                {
                    wall.RotatePlacement(1);
                }    
                int canPlace = grid.GetValidPlacement(hit.point);
                // if valid placement
                if(canPlace == 0)
                {
                    if(Input.GetKeyDown(KeyCode.X))
                    {
                        grid.SetValue(hit.point,1);
                    }
                    tempObject.transform.position = grid.SnapObjectToGrid(hit.point);
                    wall.SetMaterialColor(2);
                    
                }
                else if(canPlace == 1)
                {
                    //Debug.Log("Already filled");
                    tempObject.transform.position = grid.SnapObjectToGrid(hit.point);
                    //tempObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                    wall.SetMaterialColor(1);
                    
                }
                else if(canPlace == -1)
                {
                    //Debug.Log("Invalid");
                }
            }
        }
    }
}