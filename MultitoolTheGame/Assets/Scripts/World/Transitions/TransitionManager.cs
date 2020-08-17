using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {
    public Animator transition;

    public void StartFade(int index) {
        StartCoroutine(fade(index));
    }

    public IEnumerator fade(int index) {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        yield return null;
    }
}
