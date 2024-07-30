using Events;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.BindInstance(_camera);
        
    }
}