using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(fade(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public IEnumerator fade(int index)
    {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
