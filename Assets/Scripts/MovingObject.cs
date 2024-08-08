using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float destructionHeight;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.instance;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position.y < destructionHeight)
        {
            transform.position += Vector3.up * playerController.CurrentYSpeed * Time.deltaTime;
            transform.position += Vector3.left * playerController.CurrentXSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}