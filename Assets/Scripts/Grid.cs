using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private int depth;
    private float cellSize;
    private GameObject textParent;
    private int[,,] gridArray;
    private Vector3 originPosition;
    private TextMesh[,,] debugTextArray;
    private PlaceableBlock[,,] blocksArray;
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition)
    {
        GameObject gameObject = new GameObject("World_Text",typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent,true);
        transform.localPosition = localPosition;
        TextMesh tm = gameObject.GetComponent<TextMesh>();
        tm.text = text;
        return tm;
    }
    public Grid(int width, int height,int depth, float cellSize, Vector3 originPosition)
    {
        textParent = new GameObject("TextParent");
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new int[width, height, depth];
        blocksArray = new PlaceableBlock[width,height,depth];
        debugTextArray = new TextMesh[width, height, depth];
        for (int x =0; x< gridArray.GetLength(0);x++)
        {
            for (int y =0; y< gridArray.GetLength(1);y++)
            {
                for (int z =0; z< gridArray.GetLength(2);z++)
                {   
                    Debug.Log(" " + x + y + z);
                    debugTextArray[x,y,z] = CreateWorldText(textParent.transform,gridArray[x,y,z].ToString(), GetWorldPosition(x,y,z)+ new Vector3(cellSize,cellSize,cellSize)*0.5f);
                }
               
            }   
        }        
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition-originPosition).x/cellSize);
        y = Mathf.FloorToInt((worldPosition-originPosition).y/cellSize);
        z = Mathf.FloorToInt((worldPosition-originPosition).z/cellSize);
        
    }
    public void SetValue(int x, int y , int z, int value)
    {
        if(x>=0 && y>=0 && z>=0 && x < width &&  y < height && z < depth)
        {
            debugTextArray[x,y,z].text = "" + value;
            gridArray[x,y,z] = value;
        }
        
    }
    public void SetValue(Vector3 worldPosition, int value)
    {   
        int x,y,z;
        GetXY(worldPosition,out x, out y, out z);
        SetValue(x,y,z,value);

    }
    public int GetValidPlacement(Vector3 worldPosition)
    {
        int x,y,z;
        GetXY(worldPosition,out x, out y, out z);
        if(x>=0 && y>=0 && z>=0 && x < width &&  y < height && z < depth)
        {
            if(gridArray[x,y,z] == 0)
            {
                return 0;
            }
            else if (gridArray[x,y,z] == 1)
            {
            return 1;
            }
        }
        return -1;
        
        
    }
    private Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x,y,z) * cellSize + originPosition;
    }

    public Vector3 SnapObjectToGrid(Vector3 snapPosition)
    {
        int x, y,z;
        GetXY(snapPosition,out x,out y,out z);
        return GetWorldPosition(x,y,z);
    }
   
}
