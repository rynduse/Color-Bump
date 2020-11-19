using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float thrust = 0.01f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float wallDistance = 5f;
    [SerializeField] private float minCamDistance = 3f;
    private Touch touch;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    GameManager.singleton.StartGame();
                    if (!GameManager.singleton.GameEnded)
                    {
                            
                    }
                        break;

                case TouchPhase.Moved:
                    if (!GameManager.singleton.GameEnded)
                    {
                        transform.position = new Vector3(
                        transform.position.x + touch.deltaPosition.x * thrust,
                     transform.position.y,
                     transform.position.z + touch.deltaPosition.y * thrust);
                        transform.position = transform.position + Vector3.forward * 5 * Time.fixedDeltaTime;
                    }
                    break;
            }

        }
    }

    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }
        //if (GameManager.singleton.GameStarted)
        //{
        //    rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime);
        //}
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }

        if (transform.position.z < Camera.main.transform.position.z + minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }
        if (collision.gameObject.tag == "Death")
        {
            GameManager.singleton.EndGame(false);
        }
    }
}
