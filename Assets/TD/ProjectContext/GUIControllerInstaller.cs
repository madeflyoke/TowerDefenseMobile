using TD.GUI;
using UnityEngine;
using Zenject;

public class GUIControllerInstaller : MonoInstaller
{
    [SerializeField] private GUIController guiController;
    public override void InstallBindings()
    {
        Container.Bind<GUIController>().FromComponentInNewPrefab(guiController).AsSingle().NonLazy();
    }
}