using TD.Inputs;
using UnityEngine;
using Zenject;

public class InputsControllerInstaller : MonoInstaller
{
    [SerializeField] private InputsController inputsController;
    public override void InstallBindings()
    {
        Container.Bind<InputsController>().FromComponentInNewPrefab(inputsController).AsSingle().NonLazy();
    }
}