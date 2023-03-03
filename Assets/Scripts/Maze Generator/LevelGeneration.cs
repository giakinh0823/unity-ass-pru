using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField]
    public Transform[] startingPositions;
    [SerializeField]
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT

    private int direction;
    private bool stopGeneration;
    private int downCounter;

    [SerializeField]
    public float moveIncrement;
    private float timeBtwSpawn;
    [SerializeField]
    public float startTimeBtwSpawn;

    [SerializeField]
    public LayerMask whatIsRoom;

    [SerializeField]
    private PlayerController playerController;

    List<GameObject> roomTrues;

    private Transform startPoint;
    private Transform endPoint;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
        roomTrues = new List<GameObject>();
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        startPoint = startingPositions[randStartingPos];
        Instantiate(rooms[1], transform.position, Quaternion.identity);
        playerController.transform.position = new Vector3(startPoint.position.x, startPoint.position.y - 2f, 0f);
        playerController.gameObject.SetActive(false);
        direction = Random.Range(1, 6);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Đi sang phải !

            if (transform.position.x < 25)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                roomTrues.Add(Instantiate(rooms[randRoom], transform.position, Quaternion.identity));

                // Đảm bảo trình tạo cấp độ không di chuyển sang trái!
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Sang trái !

            if (transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                roomTrues.Add(Instantiate(rooms[randRoom], transform.position, Quaternion.identity));

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 5)
        { // Đi xuống
            downCounter++;
            if (transform.position.y > -25)
            {
                // Bây giờ tôi phải thay thế căn phòng TRƯỚC KHI đi xuống bằng căn phòng có lỗ XUỐNG, nên loại là  3 hoặc 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                Debug.Log(previousRoom);
                if (previousRoom.GetComponent<Room>().roomType != 4 && previousRoom.GetComponent<Room>().roomType != 2)
                {

                    // Vấn đề của tôi: nếu việc tạo cấp độ giảm TWICE liên tiếp, có khả năng phòng trước đó chỉ
                    // một LRB, nghĩa là không có TOP nào mở cho phòng khác !

                    if (downCounter >= 2)
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        roomTrues.Add(Instantiate(rooms[4], transform.position, Quaternion.identity));
                    }
                    else
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }
                        roomTrues.Add(Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity));
                    }

                }



                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Đảm bảo căn phòng chúng tôi ghé vào có cửa mở TOP !
                int randRoom = Random.Range(3, 5);
                roomTrues.Add(Instantiate(rooms[randRoom], transform.position, Quaternion.identity));

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
                playerController.gameObject.SetActive(true);
                endPoint = roomTrues[roomTrues.Count - 1].transform;
            }
        }
    }
}
