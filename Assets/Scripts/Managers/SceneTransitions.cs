using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneTransitions : MonoBehaviour
    {
        public void TransitionToSecondScene()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.StopSFX();
            LevelManager.Instance.ChangeSceneDirect("SecondScene");
        }
        public void TransitionToThirdScene()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.StopSFX();
            LevelManager.Instance.ChangeSceneDirect("ThirdScene");
        }
    }
}