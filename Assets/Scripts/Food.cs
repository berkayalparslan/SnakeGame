using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public void ChangePositionRandomly()
    {
        transform.position = new Vector3(Random.Range(-14, 15), Random.Range(-9, 10));
    }
}
