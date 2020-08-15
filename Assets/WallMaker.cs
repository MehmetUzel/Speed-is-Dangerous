using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wallPartPrefab;
    public Transform lastWall;
    private Vector3 lastWallPos;
    float offset = 0.70711f;
    bool cheat = false;

    Transform player { get { return FindObjectOfType<PlayerController>().transform; } }

    Camera cam;

    int random;

    void Start()
    {
        cam = Camera.main;
        lastWallPos = lastWall.position;
        InvokeRepeating("CreateWall", 0, 0.1f);
        random = 5;
    }

    private void Update()
    {
        var fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if (fingerCount > 1)
        {
            //paused = togglePause();
            cheat = true;
        }

    }

    private void CreateWall()
    {
        float distance = Vector3.Distance(player.position, lastWallPos);
        if (distance > cam.orthographicSize * 2)
        {
            return;
        }
        Vector3 newPos = Vector3.zero;
        
        if(random < 4)
        {
            newPos = new Vector3(lastWallPos.x - offset, lastWallPos.y, lastWallPos.z + offset);
        }
        else
        {
            newPos = new Vector3(lastWallPos.x + offset, lastWallPos.y, lastWallPos.z + offset);

        }

        var newBlock = Instantiate(wallPartPrefab, newPos, Quaternion.Euler(0,45,0), transform);

        if (cheat)
        {
            newBlock.transform.GetChild(0).gameObject.SetActive(cheat);

        }
        else
        {
            newBlock.transform.GetChild(0).gameObject.SetActive(random % 4 == 1);

        }
        random = Random.Range(0, 11);

        lastWallPos = newBlock.transform.position;

    }
}
