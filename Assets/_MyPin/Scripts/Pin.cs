using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pin : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 1f;
    // Start is called before the first frame update

    private bool IsPinned = false;

    private bool IsLaunchered = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (IsPinned == false && IsLaunchered == true)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsPinned = true;
        if (collision.gameObject.tag == "TargetCircle")
        {
            GameObject childObject = transform.Find("Square").gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.SetParent(collision.gameObject.transform);
            GameManager.instance.DecreaseGoal();
        }else if(collision.gameObject.tag == "Pin")
        {
            Destroy(collision.gameObject);
            GameManager.instance.SetGameOver(false);
        }
    }
    public void Launch()
    {
        IsLaunchered = true;
    }
}
