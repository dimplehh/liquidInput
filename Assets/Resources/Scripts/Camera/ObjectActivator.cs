using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] string activatorTag = null;
    [SerializeField] bool deactivateOnExit = false;
    [SerializeField] GameObject[] objects = null;
    [SerializeField] LightColorController lightColorController;
    [SerializeField] float time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(activatorTag))
        {
            foreach (var obj in objects)
                obj.SetActive(true);
            StartCoroutine("UpdateColor");
        }
    }

    IEnumerator UpdateColor()
    {
        if(lightColorController.time < time)
        {
            while (lightColorController.time < time)
            {
                lightColorController.time += 0.05f;
                lightColorController.UpdateSetters();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (deactivateOnExit && collision.CompareTag(activatorTag))
        {
            foreach (var obj in objects)
                obj.SetActive(false);
        }
    }
}
