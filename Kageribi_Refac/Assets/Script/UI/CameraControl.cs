using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= 150)
        {
            this.GetComponent<Cameramove>().enabled = false;

            if (Camera.orthographicSize < 10)
            {
                Camera.orthographicSize += 0.1f;
                this.transform.Translate(0.25f, 0.1f, 0f);
            }
        }
    }
}
