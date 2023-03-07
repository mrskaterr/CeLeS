using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Morph : NetworkBehaviour
{
    [SerializeField] AudioSource changingsound;
    [Networked(OnChanged = nameof(OnIndexChange))]
    public int index { get; set; } = -1;

    [SerializeField] private GameObject eyes;
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private List<GameObject> morphingObjects = new List<GameObject>();
    [SerializeField] private LocalCameraHandler cameraHandler;

    private static void OnIndexChange(Changed<Morph> _changed)
    {
        _changed.Behaviour.SetMorph();
    }

    private void SetMorph()
    {
        eyes.SetActive(index == -1);
        mr.enabled = index == -1;
        cameraHandler.ChangePerspective(index);
        for (int i = 0; i < morphingObjects.Count; i++)
        {
            morphingObjects[i].SetActive(index == i);
        }
        changingsound.Play();
    }
}