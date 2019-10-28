using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float minX; 

	void Update ()
    {
		if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
	}
}
