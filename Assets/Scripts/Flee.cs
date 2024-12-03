using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;

public class ExampleClass : MonoBehaviour

{
    public void LoadMain()
    {
        Loader.Load(Loader.Scene.Main);
    }

	void Start() {
		Button itemBut = gameObject.GetComponent<Button>();
		if (itemBut){ // ensure button component exists
			// Adds function call as listener to button
			itemBut.onClick.AddListener(delegate {
				Loader.Load(Loader.Scene.Main);
			});
		}
	}
}
