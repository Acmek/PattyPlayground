using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCode : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject[] children;
    [SerializeField] private string layer;
    
    void OnMouseDown() {
        GameObject objectSpawned = Instantiate(spawnObject, spawnPos, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
        objectSpawned.name = gameObject.name;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        gameObject.layer = LayerMask.NameToLayer(layer);

        for(int i = 0; i < children.Length; i++)
            children[i].layer = LayerMask.NameToLayer(layer);

        GetComponent<Rigidbody2D>().gravityScale = 2;
        Destroy(this);
    }
}
