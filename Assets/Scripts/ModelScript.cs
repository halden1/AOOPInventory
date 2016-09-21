using UnityEngine;
using System.Collections;

public class ModelScript : MonoBehaviour {
    public float mooseSensitivity = .4f;
    private Vector3 mooseRef;
    private Vector3 mooseOffset;
    private Vector3 rot;
    private bool isRotting;
    public float rotSpeed = 10;
    public float dampRot = -.75f;
	// Use this for initialization
	void OnMouseDown () {
        isRotting = true;
        mooseRef = Input.mousePosition;
	}
	
	// Update is called once per frame
	void OnMouseUp ()
    {
        isRotting = false;
    }
    void Update()
    {
        //Debug.Log("No errors, Halp");
        if (isRotting)
        {
            mooseOffset = Input.mousePosition - mooseRef;
            rot.y = (mooseOffset.x + mooseOffset.y) * -mooseSensitivity;
            transform.Rotate(rot);
            mooseRef = Input.mousePosition;
        }
        else
        {
            float sin = rot.y < 0 ? -1 : 1;
            rot.y -= dampRot * sin * Time.deltaTime;
            rot.y = Mathf.Abs(rot.y) <= 1 ? sin : rot.y;
            transform.rotation = Quaternion.AngleAxis(rotSpeed * rot.y, Vector3.up);
        }

    }
}
