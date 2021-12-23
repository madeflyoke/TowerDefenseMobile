// GENERATED AUTOMATICALLY FROM 'Assets/TD/Inputs/InputSystem/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""General"",
            ""id"": ""e3ba17dc-52c2-42d3-a208-2d608e093991"",
            ""actions"": [
                {
                    ""name"": ""Camera"",
                    ""type"": ""Button"",
                    ""id"": ""4f0f0f4c-ed74-40c6-9175-f97516312e8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectPosition"",
                    ""type"": ""Value"",
                    ""id"": ""d3d7c707-65a4-49f6-8e82-a9269a146a89"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""2f97fe41-4e74-404f-a98b-7b3a2a37de8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Directions"",
                    ""id"": ""145dc86b-f974-4032-ac26-b410f078e1a6"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f6be0d7-a9c5-43e3-bc93-48d5d60008c8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""209076ca-fb71-4dd2-a01e-ae7343f2aa63"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ed9d8dd8-4b0d-40f4-bf42-7a4fbcd0c500"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3c136ebf-ab11-4b05-874f-ffb1063a61fa"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5bca861-8f6d-4bb1-8c88-50116f9f1857"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""SelectPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d5dafc4-503b-483e-8e4e-e29177929653"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(pressPoint=0.3,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and mouse"",
            ""bindingGroup"": ""Keyboard and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Camera = m_General.FindAction("Camera", throwIfNotFound: true);
        m_General_SelectPosition = m_General.FindAction("SelectPosition", throwIfNotFound: true);
        m_General_Select = m_General.FindAction("Select", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Camera;
    private readonly InputAction m_General_SelectPosition;
    private readonly InputAction m_General_Select;
    public struct GeneralActions
    {
        private @PlayerInputs m_Wrapper;
        public GeneralActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Camera => m_Wrapper.m_General_Camera;
        public InputAction @SelectPosition => m_Wrapper.m_General_SelectPosition;
        public InputAction @Select => m_Wrapper.m_General_Select;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Camera.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCamera;
                @SelectPosition.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelectPosition;
                @SelectPosition.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelectPosition;
                @SelectPosition.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelectPosition;
                @Select.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @SelectPosition.started += instance.OnSelectPosition;
                @SelectPosition.performed += instance.OnSelectPosition;
                @SelectPosition.canceled += instance.OnSelectPosition;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);
    private int m_KeyboardandmouseSchemeIndex = -1;
    public InputControlScheme KeyboardandmouseScheme
    {
        get
        {
            if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
            return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
        }
    }
    public interface IGeneralActions
    {
        void OnCamera(InputAction.CallbackContext context);
        void OnSelectPosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
