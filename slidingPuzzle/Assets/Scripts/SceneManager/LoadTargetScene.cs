using UnityEngine;
namespace SceneManager
{
    public class LoadTargetScene : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}