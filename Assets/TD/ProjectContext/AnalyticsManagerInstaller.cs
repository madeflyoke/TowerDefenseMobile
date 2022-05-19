using Zenject;
using TD.Services.Firebase;

public class AnalyticsManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(new AnalyticsManager()).AsSingle().NonLazy();
    }
}
