using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    [SerializeField] KeyCode key;
    Color old;
    Image image;
    bool active = false;
    GameObject note;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if( Input.GetKeyDown(key) && active)
        {
            Destroy(note);
            StartCoroutine(Pressed());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if(collision.gameObject.tag == "Note")//getcomponent note
            note=collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
    IEnumerator Pressed()
    {
        old = image.color;
        image.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        image.color = old;
    }
}
