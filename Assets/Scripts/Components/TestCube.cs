using Events;
using UnityEngine;
using Zenject;

namespace Components
{
    public class TestCube : MonoBehaviour
    {
        [Inject] private ProjectEvents ProjectEvents { get; set;}

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ProjectEvents.ProjectStarted += onProjectInstalled;
        }

        private void onProjectInstalled()
        {
            Debug.LogWarning("var");
        }


        private void UnRegisterEvents()
        {
            ProjectEvents.ProjectStarted -= onProjectInstalled;
        }
    }
}
