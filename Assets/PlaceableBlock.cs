using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour
{
   private int width;
   private int height;
   private int depth;

   public GameObject prefabObject;
   [SerializeField] public Material redInvalid;
   [SerializeField] public Material greenValid;
    private Transform childCenter;
    public void Awake()
    {
        Debug.Log(this.transform.name);
        childCenter = this.transform.GetChild(0);
    }
    public void setDimensions(int x, int y, int z)
    {
        this.width =x;
        this.height =y;
        this.depth =z;
        
    }
    public void RotatePlacement(int direction)
    {
        if(direction ==1)
        {
            childCenter.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
        }
        else if(direction ==-1)
        {
            childCenter.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
        }
        
    }
    private void SwapWidthDepth()
    {
        int tempvalue = width;
        this.width = this.depth;
        this.depth = width;
    }

    public void SetMaterialColor(int mat)
    {
        if(mat == 1)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = redInvalid;
            //set red color for all children
        }
        else if(mat ==2)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = greenValid;
        }
    }

    

}
