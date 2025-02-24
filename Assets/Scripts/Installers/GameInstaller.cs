using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameStateManager>().To<GameStateManager>().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
    }
}