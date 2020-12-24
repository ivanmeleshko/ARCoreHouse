using UnityEngine;
using GoogleARCore;
using GoogleARCore.HelloAR;
using UnityEngine.UI;

public class InstantiateObjectOnTouch : MonoBehaviour
{

    public Camera FirstPersonCamera;
    public GameObject PlaceGameObject;
    private GameObject placedObject;
    private GameObject building;
    private Vector3 firstpoint;    //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f;   //angle for axes x for rotation
    private float xAngTemp = 0.0f; //temp variable for angle
    private float zoomSpeed = 0.22f;
//test

    void Update ()
    {
        // Get the touch position to see if we have at least one touch event currently active
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Now that we know that we have an active touch point, do a raycast to see if it hits
        // a plane where we can instantiate the object on.
        TrackableHit hit;
        var raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit) && PlaceGameObject != null && placedObject == null)
        {
            // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
            // world evolves.
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // Intanstiate a game object as a child of the anchor; its transform will now benefit
            // from the anchor's tracking.
            placedObject = Instantiate(PlaceGameObject, hit.Pose.position, hit.Pose.rotation);

            // Game object should look at the camera but still be flush with the plane.
            if ((hit.Flags & TrackableHitFlags.PlaneWithinPolygon) != TrackableHitFlags.None)
            {
                // Get the camera position and match the y-component with the hit position.
                Vector3 cameraPositionSameY = FirstPersonCamera.transform.position;
                cameraPositionSameY.y = hit.Pose.position.y;

                // Have game object look toward the camera respecting his "up" perspective, which may be from ceiling.
                placedObject.transform.LookAt(cameraPositionSameY, placedObject.transform.up);

                building = GameObject.Find("Building");
            }

            // Make the newly placed object a child of the parent
            placedObject.transform.parent = anchor.transform;
        }
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
