using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour {

	public void RestartScene()
	{
		Debug.Log(" ");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
