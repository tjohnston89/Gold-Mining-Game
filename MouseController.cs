using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject dragSelectionBox;

    bool digMode = false;

    Tile.TileType buildModeTile = Tile.TileType.Dirt;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // The world-position start of our left-mouse drag operation
    Vector3 dragStartPosition;
    List<GameObject> dragPreviewGameObjects;

    


    // Use this for initialization
    void Start()
    {
        dragPreviewGameObjects = new List<GameObject>();

        //new testMovement();
        
    }

    // Update is called once per frame
    void Update () {

        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        //UpdateCursor();
        UpdateDragging();
        UpdateCameraMovement();


        
        

        // Save the mouse position from this frame
        // We don't use currFramePosition because we may have moved the camera.
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    public void UpdateDragging()
    {
        // If we're over a UI element, then bail out from this.
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //     return;
        // }

        // Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }

        int start_x = Mathf.FloorToInt(dragStartPosition.x);
        int end_x = Mathf.FloorToInt(currFramePosition.x);
        int start_y = Mathf.FloorToInt(dragStartPosition.y);
        int end_y = Mathf.FloorToInt(currFramePosition.y);

        // We may be dragging in the "wrong" direction, so flip things if needed.
        if (end_x < start_x)
        {
            int tmp = end_x;
            end_x = start_x;
            start_x = tmp;
        }
        if (end_y < start_y)
        {
            int tmp = end_y;
            end_y = start_y;
            start_y = tmp;
        }

        // Clean up old drag previews
        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }



            if (Input.GetMouseButton(0))
        {
            // Display a preview of the drag area
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        // Display the building hint on top of this tile position
                        GameObject go = SimplePool.Spawn(dragSelectionBox, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

        
            // Loop through all the tiles
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        if (digMode)
                        {
                         Vector3 newTarget = new Vector3(start_x, start_y, 0);
                         GameObject vehicle = GameObject.Find("Excavator").gameObject;
                         StartCoroutine (moveToTarget(newTarget));
                         
                         t.Type = buildModeTile;
                         
                        }
                        
                        
                            
                            

                            
                        
                        if (t == null){
                            Debug.Log("Invalid Tile");
                        }
                    }
                        Debug.Log("Clicked tile is at X: " + t.X + " Y: " + t.Y);

                        





                        //currentGold = t.gold;

                        /* if (buildModeIsObjects == true)
                         {
                             //Create the installed object and assign it to the tile

                             WorldController.Instance.World.PlaceInstalledObject(buildModeObjectType, t);



                         }
                         else
                         {
                             //Tile changing mode
                             t.Type = buildModeTile;
                         }*/
                    
                }
            }
        }
        
    }


    void UpdateCameraMovement()
    {
        // Handle screen panning
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {   // Right or Middle Mouse Button

            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
    }

    public void SetMode_Dig()
    {
        digMode = true;
        buildModeTile = Tile.TileType.Dirt;
        //buildModeIsObjects = false;
    }

     
     public IEnumerator moveToTarget(Vector3 newTarget)
     {
        
        
        float speed = .5f;
        Vector3 target = newTarget;
		GameObject vehicle = GameObject.Find("Excavator").gameObject;

        vehicle.AddComponent<testMovement>().target = target;
        bool isMoving = vehicle.GetComponent<testMovement>().isMoving;

        Debug.Log(isMoving);
        yield return new WaitUntil(() => vehicle.transform.position == newTarget);
        

        
            
            
            
            
       
        

        
     }
     public IEnumerator doWork()
     {
         
                           float work = 20;
                           float workSpeed = 1;

                           workSpeed = workSpeed * Time.deltaTime;
                            for ( float workLeft = work; workLeft >= 0;)
                            {

                                workLeft = workLeft - workSpeed;

                                
                                Debug.Log(workLeft);
                                //digging();
                                
                                if(workLeft <= 0)
                                {
                                Debug.Log("Work done");
                                yield return null;
                               // t.Type = buildModeTile;
                               

                                }
                               

                                


                            }
     }

         
     
        //Debug.Log(vehicle.name);
        //Debug.Log(vehicle.transform);
        



		
/* 
        while (true)
        {
        float step = speed * Time.deltaTime;       
		vehicle.transform.position = Vector3.Lerp(vehicle.transform.position, target, step);
		Debug.Log(vehicle.transform.position);
        
         if (vehicle.transform.position == target) //add a check here or in the "while" to break out of the loop!
         {
             yield return "done";
             break;
            
         }
        } */
        
        
       
        

        
    




                    //if (HasWater(tilemap, nPos))
                    //{
                    //    tilemap.RefreshTile(nPos);
                    //}

                
            
        
    }
