using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleClass : MonoBehaviour

{
    public void LoadMain()
    {
        Loader.Load(Loader.Scene.Main);
    }
}
