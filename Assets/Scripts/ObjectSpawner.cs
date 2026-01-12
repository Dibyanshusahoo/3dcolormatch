using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int minSpawn = 10;
    public int maxSpawn = 15;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int count = Random.Range(minSpawn, maxSpawn + 1);

        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = GetRandomPointInBox();
            Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], randomPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPointInBox()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 center = box.center + transform.position;
        Vector3 size = box.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        float y = transform.position.y + 1f; 

        return new Vector3(x, y, z);
    }
}
