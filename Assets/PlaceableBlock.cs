using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour
{
    public int width;
    public int height;
    public int depth;

    public int widthCoefficient = 1;
    public int depthCoefficient = 1;
    public GameObject prefabObject;
    public int[] originPosition;
    public List<int[]> blockPositions;
   [SerializeField] Material redInvalid;
   [SerializeField] Material greenValid;
   [SerializeField] Material transparent;
    private Transform childcubeCenter;
    private Transform childobjectCenter;
    public void Awake()
    {
        Debug.Log(this.transform.name);
        childcubeCenter = this.transform.GetChild(0);
        childobjectCenter = this.transform.GetChild(1);
        blockPositions = new List<int[]>();
    }
    public void setDimensions(int x, int y, int z)
    {
        this.width =x;
        this.height =y;
        this.depth =z;
        
    }
    public void getDimensions(out int x, out int y, out int z)
    {
        x = this.width;
        y = this.height;
        z = this.depth;
        
    }
    private void calculateWDCoefficients()
    {
        
        if(childobjectCenter.transform.localEulerAngles.y == 0)
        {
            Debug.Log(new Vector3(3f * this.width/2.0f, 3f * this.height/2.0f, 3f * this.depth/2.0f));
            childcubeCenter.transform.localPosition = new Vector3(3f * this.width/2.0f, 3f * this.height/2.0f, 3f * this.depth/2.0f);
            childcubeCenter.transform.localScale = new Vector3(this.width * 3f, this.height *3f, this.depth *3f);
            widthCoefficient =1;
            depthCoefficient = 1;
        }
        else if(childobjectCenter.transform.localEulerAngles.y == 90)
        {
            childcubeCenter.transform.localPosition = new Vector3(3f * this.width/2.0f, 3f * this.height/2.0f, 3f * this.depth/2.0f);
            childcubeCenter.transform.localScale = new Vector3(this.width * 3f, this.height *3f, this.depth *3f);
            widthCoefficient =1;
            depthCoefficient = 1;
        }
        else if(childobjectCenter.transform.localEulerAngles.y == 180)
        {   
            childcubeCenter.transform.localPosition = new Vector3(3f * this.width/2.0f, 3f * this.height/2.0f, 0f * this.depth/2.0f);
            childcubeCenter.transform.localScale = new Vector3(this.width * 3f, this.height *3f, this.depth *3f);
            widthCoefficient = -1;
            depthCoefficient = -1;
        }
        else if(childobjectCenter.transform.localEulerAngles.y == 270)
        {
            childcubeCenter.transform.localPosition = new Vector3(0f * this.width/2.0f, 3f * this.height/2.0f, 3f * this.depth/2.0f);
            childcubeCenter.transform.localScale = new Vector3(this.width * 3f, this.height *3f, this.depth *3f);
            widthCoefficient = -1;
            depthCoefficient = 1;
        }
    }
    public void RotatePlacement(int direction)
    {
        if(direction ==1)
        {
            childobjectCenter.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
            
        }
        else if(direction ==-1)
        {
            childobjectCenter.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
            
        }
        SwapWidthDepth();
        Debug.Log ("Swapped");
        calculateWDCoefficients();
        Debug.Log("Calculated New WD coef");
       
        
    }
    private void SwapWidthDepth()
    {
        int tempvalue = width;
        this.width = this.depth;
        this.depth = tempvalue;
    }

    public void SetMaterialColor(int mat)
    {
        if(mat == 1)
        {
            childcubeCenter.gameObject.GetComponent<Renderer>().material = redInvalid;
            //set red color for all children
        }
        else if(mat ==2)
        {
            childcubeCenter.gameObject.GetComponent<Renderer>().material = greenValid;
        }
        else if(mat ==3)
        {
            childcubeCenter.gameObject.GetComponent<Renderer>().material = transparent;
        }
    }
    public void ActivateColliders()
    {
        //TODO
    }
    
}
