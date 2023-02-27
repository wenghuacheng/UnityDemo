using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPointMove : MonoBehaviour
{
    private List<Transform> WayPoint = new List<Transform>();
    public int currentIndex = 0;

    public Transform Parent;

    // Start is called before the first frame update
    void Start()
    {
        var transforms = Parent.GetComponentsInChildren<Transform>();
        this.WayPoint = transforms.Select(p => p.transform).ToList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (WayPoint.Count <= 0) return;

        var point = this.WayPoint[currentIndex].position;
        Debug.Log(point);

        this.transform.position = Vector3.MoveTowards(this.transform.position, point, 1 * Time.fixedDeltaTime);

        if (Vector3.Distance(this.transform.position, point) <= 0.05f)
        {
            currentIndex++;
            currentIndex = currentIndex % this.WayPoint.Count;
        }

    }
}
