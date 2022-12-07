using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromComponentInNewPrefabResource("SceneLoader").AsSingle().NonLazy();
        Container.Bind<GameSession>().FromComponentInNewPrefabResource("GameSession").AsSingle().NonLazy();
    }
}