using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, GameManager.instance.beatTempo * Time.deltaTime, 0f);

        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                GameManager.instance.NoteHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
            Destroy(gameObject);
        }
    }
}
