using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public bool isColored = false;
    public void ColorChange(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;
        
        GameManager.Singleton.CheckComplete();
    }
}
