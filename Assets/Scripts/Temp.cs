using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public GameObject placedObject;
    private GameObject building;
    private Vector3 firstpoint;    //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f;   //angle for axes x for rotation
    private float xAngTemp = 0.0f; //temp variable for angle
    private float zoomSpeed = 0.22f;
    //test

    void Update()
    {
       
    }


    void LateUpdate()
    {
        if (placedObject != null)
        {
            if (!UI.IsPointerOverUIObject())
            {
                xAngle = placedObject.transform.eulerAngles.y;

                //Check count touches
                if (Input.touchCount == 1)
                {
                    //Touch began, save position
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(0).position;
                        xAngTemp = xAngle;
                    }
                    //Move finger
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(0).position;
                        xAngle = xAngTemp - (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                        placedObject.transform.rotation = Quaternion.AngleAxis(xAngle, Vector3.up);
                    }
                }

                //Pinch zoom
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                    float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                    if (deltaMagDiff > 0)
                    {
                        if (placedObject.transform.localScale.x > 0.0029f)
                            placedObject.transform.localScale = placedObject.transform.localScale / 1.05f;// * deltaMagDiff * zoomSpeed;
                    }
                    else
                    {
                        if (placedObject.transform.localScale.x < 0.12f)
                            placedObject.transform.localScale = placedObject.transform.localScale * 1.05f;
                    }
                }
            }

            if (HouseConfig.materialChanged)
            {
                building = GameObject.Find("Building");
                building.GetComponent<MeshRenderer>().material = HouseConfig.material;
                HouseConfig.materialChanged = false;
            }
        }
    }


    public void Clear()
    {
        if (placedObject != null)
        {
            Destroy(placedObject);
        }
    }

}
