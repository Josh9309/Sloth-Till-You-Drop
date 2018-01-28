using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour {

    public int maxLeafs = 3;
    public float spawnTime = 2.0f;
    private float spawnTimer = 0.0f;
    [SerializeField] private GameObject leafPrefab;
    public List<GameObject> leafs;

    // Use this for initialization
    void Start () {
        float xPos = Random.Range(0.2f, 0.8f);

        Vector3 leafPos = Camera.main.ViewportToWorldPoint(new Vector3(xPos, 1, 0));
        GameObject leaf = Instantiate(leafPrefab, new Vector3(leafPos.x, leafPos.y+5, -1), Quaternion.identity);
        leafs.Add(leaf);
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnTimer > spawnTime && leafs.Count < maxLeafs)
            SpawnLeaf();
        spawnTimer += Time.deltaTime;
	}

    private void SpawnLeaf()
    {
        float xPos = Random.Range(0.2f, 0.8f);

        Vector3 leafPos = Camera.main.ViewportToWorldPoint(new Vector3(xPos, 1, 0));
        GameObject leaf = Instantiate(leafPrefab, new Vector3(leafPos.x, leafPos.y + 5, -1), Quaternion.identity);
        leafs.Add(leaf);
        spawnTimer = 0.0f;
    }
}
