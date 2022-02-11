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
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""18381fc8-bb7c-4309-a4d2-36dde283e9ca"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Value"",
                    ""id"": ""5f9a77c4-ca30-4ea6-bed7-9687a404d2ed"",
                    ""expectedControlType"": ""Vector2"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""749d38ba-a0d0-498a-a54a-fc106023fefa"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""PlusMinusZoom"",
                    ""id"": ""25c2ced2-1e3d-40bf-8c12-111661dd6481"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""69f23ae4-23da-466b-8b0b-ae86fffaa59a"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4efe0c1e-3ae6-4c38-82b6-c187eb1fe7a0"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f8312789-02e6-4650-9f5f-9631668ba73a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TouchInput"",
            ""id"": ""240ab750-7e7d-4900-aa58-762cccc04c94"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""1a6b5380-f254-41e2-8b40-3c903086202f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoomFirstPosition"",
                    ""type"": ""Value"",
                    ""id"": ""5e63524d-c558-450e-bf98-7260db46762a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoomStart"",
                    ""type"": ""Button"",
                    ""id"": ""64af74de-2a51-4c00-b48b-e67d932d9c5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""CameraZoomSecondPosition"",
                    ""type"": ""Value"",
                    ""id"": ""ab4a57c3-10b1-4f50-8178-6a1fd0e6b467"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovementStart"",
                    ""type"": ""Button"",
                    ""id"": ""257a7b27-9c20-4d9b-add4-a1a554400a27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cb853b19-96e9-4229-a148-ad4e617b95a8"",
                    ""path"": ""<Touchscreen>/touch0/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""091d2462-2404-462d-8adb-e0f659b941b1"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""CameraZoomFirstPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d53cbcb3-c9e6-4a51-ba16-25de9bed6b57"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""CameraZoomStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d355760b-b237-458e-9d26-191c976dfb4b"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""CameraZoomSecondPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66640eab-205c-4ad4-9240-39d3056686b8"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""CameraMovementStart"",
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
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
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
        m_General_CameraZoom = m_General.FindAction("CameraZoom", throwIfNotFound: true);
        m_General_Test = m_General.FindAction("Test", throwIfNotFound: true);
        // TouchInput
        m_TouchInput = asset.FindActionMap("TouchInput", throwIfNotFound: true);
        m_TouchInput_CameraMovement = m_TouchInput.FindAction("CameraMovement", throwIfNotFound: true);
        m_TouchInput_CameraZoomFirstPosition = m_TouchInput.FindAction("CameraZoomFirstPosition", throwIfNotFound: true);
        m_TouchInput_CameraZoomStart = m_TouchInput.FindAction("CameraZoomStart", throwIfNotFound: true);
        m_TouchInput_CameraZoomSecondPosition = m_TouchInput.FindAction("CameraZoomSecondPosition", throwIfNotFound: true);
        m_TouchInput_CameraMovementStart = m_TouchInput.FindAction("CameraMovementStart", throwIfNotFound: true);
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
    private readonly InputAction m_General_CameraZoom;
    private readonly InputAction m_General_Test;
    public struct GeneralActions
    {
        private @PlayerInputs m_Wrapper;
        public GeneralActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Camera => m_Wrapper.m_General_Camera;
        public InputAction @SelectPosition => m_Wrapper.m_General_SelectPosition;
        public InputAction @Select => m_Wrapper.m_General_Select;
        public InputAction @CameraZoom => m_Wrapper.m_General_CameraZoom;
        public InputAction @Test => m_Wrapper.m_General_Test;
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
                @CameraZoom.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCameraZoom;
                @Test.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnTest;
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
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

    // TouchInput
    private readonly InputActionMap m_TouchInput;
    private ITouchInputActions m_TouchInputActionsCallbackInterface;
    private readonly InputAction m_TouchInput_CameraMovement;
    private readonly InputAction m_TouchInput_CameraZoomFirstPosition;
    private readonly InputAction m_TouchInput_CameraZoomStart;
    private readonly InputAction m_TouchInput_CameraZoomSecondPosition;
    private readonly InputAction m_TouchInput_CameraMovementStart;
    public struct TouchInputActions
    {
        private @PlayerInputs m_Wrapper;
        public TouchInputActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMovement => m_Wrapper.m_TouchInput_CameraMovement;
        public InputAction @CameraZoomFirstPosition => m_Wrapper.m_TouchInput_CameraZoomFirstPosition;
        public InputAction @CameraZoomStart => m_Wrapper.m_TouchInput_CameraZoomStart;
        public InputAction @CameraZoomSecondPosition => m_Wrapper.m_TouchInput_CameraZoomSecondPosition;
        public InputAction @CameraMovementStart => m_Wrapper.m_TouchInput_CameraMovementStart;
        public InputActionMap Get() { return m_Wrapper.m_TouchInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchInputActions set) { return set.Get(); }
        public void SetCallbacks(ITouchInputActions instance)
        {
            if (m_Wrapper.m_TouchInputActionsCallbackInterface != null)
            {
                @CameraMovement.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovement;
                @CameraZoomFirstPosition.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomFirstPosition;
                @CameraZoomFirstPosition.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomFirstPosition;
                @CameraZoomFirstPosition.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomFirstPosition;
                @CameraZoomStart.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomStart;
                @CameraZoomStart.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomStart;
                @CameraZoomStart.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomStart;
                @CameraZoomSecondPosition.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomSecondPosition;
                @CameraZoomSecondPosition.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomSecondPosition;
                @CameraZoomSecondPosition.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraZoomSecondPosition;
                @CameraMovementStart.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovementStart;
                @CameraMovementStart.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovementStart;
                @CameraMovementStart.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnCameraMovementStart;
            }
            m_Wrapper.m_TouchInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraZoomFirstPosition.started += instance.OnCameraZoomFirstPosition;
                @CameraZoomFirstPosition.performed += instance.OnCameraZoomFirstPosition;
                @CameraZoomFirstPosition.canceled += instance.OnCameraZoomFirstPosition;
                @CameraZoomStart.started += instance.OnCameraZoomStart;
                @CameraZoomStart.performed += instance.OnCameraZoomStart;
                @CameraZoomStart.canceled += instance.OnCameraZoomStart;
                @CameraZoomSecondPosition.started += instance.OnCameraZoomSecondPosition;
                @CameraZoomSecondPosition.performed += instance.OnCameraZoomSecondPosition;
                @CameraZoomSecondPosition.canceled += instance.OnCameraZoomSecondPosition;
                @CameraMovementStart.started += instance.OnCameraMovementStart;
                @CameraMovementStart.performed += instance.OnCameraMovementStart;
                @CameraMovementStart.canceled += instance.OnCameraMovementStart;
            }
        }
    }
    public TouchInputActions @TouchInput => new TouchInputActions(this);
    private int m_KeyboardandmouseSchemeIndex = -1;
    public InputControlScheme KeyboardandmouseScheme
    {
        get
        {
            if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
            return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IGeneralActions
    {
        void OnCamera(InputAction.CallbackContext context);
        void OnSelectPosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
    }
    public interface ITouchInputActions
    {
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraZoomFirstPosition(InputAction.CallbackContext context);
        void OnCameraZoomStart(InputAction.CallbackContext context);
        void OnCameraZoomSecondPosition(InputAction.CallbackContext context);
        void OnCameraMovementStart(InputAction.CallbackContext context);
    }
}
