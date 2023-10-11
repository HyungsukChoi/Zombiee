using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform fireTr;
    public GameObject bulletPrefab;
    public float playerSpeed;
    public float playerHp;

    // Start is called before the first frame update

    void Start()
    {

    }

    void PlayerMove()
    {
        float moveZ = Input.GetAxis("Horizontal");
        float moveX = Input.GetAxis("Vertical");

        transform.Translate(moveX, 0, -moveZ);
     }

    void bulletFire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("FIRE");
            Instantiate(bulletPrefab, fireTr.position, fireTr.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        bulletFire();
    }
}
