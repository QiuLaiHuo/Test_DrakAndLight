using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    public Transform Left;
    public Transform Right;


    public void ToLeft ()
    {
        transform.localPosition = new Vector3 (Left.localPosition.x,Left.localPosition.y,0);
            
            }


    public void ToRight()
    {
        transform.localPosition = new Vector3 (Right.localPosition.x,Right.localPosition.y,0);
    }
}
