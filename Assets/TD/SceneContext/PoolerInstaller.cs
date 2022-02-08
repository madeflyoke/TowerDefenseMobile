using TD.GamePlay.Managers;
using UnityEngine;
using Zenject;

public class PoolerInstaller : MonoInstaller
{
    [SerializeField] private Pooler pooler;
    public override void InstallBindings()
    {
        Container.Bind<Pooler>().FromInstance(pooler).AsSingle().NonLazy();
    }
}