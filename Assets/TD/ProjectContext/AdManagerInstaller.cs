using UnityEngine;
using Zenject;
using TD.Ad;

public class AdManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(new AdManager()).AsSingle().NonLazy();
    }
}