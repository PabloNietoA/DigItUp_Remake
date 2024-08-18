using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float destructionHeight;
    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position.y < destructionHeight)
        {
            transform.position += Vector3.up * PlayerController.instance.CurrentYSpeed * Time.deltaTime;
            transform.position += Vector3.left * PlayerController.instance.CurrentXSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}