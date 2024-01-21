using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    private List<GameObject> _instances = new List<GameObject>();

    public void Spawn()
    {
        var inst = Instantiate(_prefab, transform.position, Quaternion.identity);
        
        if (inst.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(new Vector3(Random.value - 0.5f, 0, 1).normalized * Random.value * 10, ForceMode.Impulse);
        }

        _instances.Add(inst);
    }

    public void Clear()
    {
        foreach(var c in _instances)
        {
            Destroy(c);
        }
        _instances.Clear();
    }
}
