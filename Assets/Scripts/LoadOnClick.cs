using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//got this from https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/creating-a-scene-menu
public class LoadOnClick : MonoBehaviour
{
    public void LoadScene(int stage)
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene(stage);
    }
	public void QuitGame() {
		Application.Quit();
	}
}
