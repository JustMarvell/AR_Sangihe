using UnityEngine;

public class Obstacle_Tugas : MonoBehaviour
{
    public float speed = 3f;
    

    void Start()
    {

    }

    void Update()
    {
        transform.position = transform.right * speed * Time.deltaTime;

        if (transform.position.x <= -2f)
        {
            Destroy(this);
        }
    }
}
