using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaptopMission : MissionObject,IInteractable
{
    [SerializeField] List<Transform> Point;
    Transform random;

    public void Interact(GameObject Object)
    {
        mission.NextStep();
    }

    void Awake()
    {
        //Debug.Log("Test: " + mission.title); nie losowało xd
        random=Point[Random.Range(0,Point.Count)];
        transform.SetParent(random);
        transform.position=random.position;
    }

    void Update()
    {

    }
}
