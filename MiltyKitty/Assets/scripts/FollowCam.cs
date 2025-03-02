using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour

{
    public GameObject theplayer;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(theplayer.transform.position.x, theplayer.transform.position.y, transform.position.z);
        
    }
}
