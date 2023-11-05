//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""8f79cf0a-1ae8-4c2e-a28a-fc8bbe88b5ef"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f0ba6613-2a9c-4841-873d-3d61821a9758"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a9ad50b2-6931-4c05-a348-b3b30f7cd2cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NewGame"",
                    ""type"": ""Button"",
                    ""id"": ""8ce27c6d-b4fe-4377-a7e2-796a6f09e50d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""157e07cd-ab5a-4479-8e99-887eb3298a7d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""15bbd150-7559-4d75-aad2-189108b69cc7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""74fafdda-79f1-4a9f-b4ac-1abf80e690b3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""51c3acc3-13c2-4462-a75f-981840cfbd74"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""463e4456-2e22-4caf-ba1e-f3cf5da3c645"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""89247e4d-3953-4a69-8995-cab9eb704ef5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a21d12ca-ef8d-4624-a1d3-4031a208a81f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3b5e03b-c7d4-4aa5-80ba-bdc448492b13"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc818a56-fa72-4a43-831a-67f76853bf01"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3479b0b0-f491-4ce5-ba16-4a4ca20ed851"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""NewGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb82d3cf-8bfa-4d73-9959-0928915062b0"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard;LeftKeyboard"",
                    ""action"": ""NewGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""58bbc207-6f0d-4196-ac1f-15b31c2a8f84"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a5790b97-93a5-4096-837e-8068bdd2e8f0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9dc68bec-05b2-41c2-98d7-e04a89eeb964"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd7d5ec0-eea9-41e0-9b6f-d59591c449e5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1419d2a4-d4b9-4408-bd23-03b803ba1f44"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Ghost"",
            ""id"": ""47639f52-9067-41b1-a15a-66e0f539b556"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""312fbaa8-d260-45f9-9a45-9f562354feed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""NewGame"",
                    ""type"": ""Button"",
                    ""id"": ""83524b55-309a-4e68-a44c-89cb062702c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FloatUp"",
                    ""type"": ""Button"",
                    ""id"": ""5d7ba8d9-989d-4ac4-b77f-c4eab25fb531"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FloatDown"",
                    ""type"": ""Button"",
                    ""id"": ""d0ed7042-3a35-4e33-aa37-7cd39974a85c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleLight"",
                    ""type"": ""Button"",
                    ""id"": ""bd5e7279-76d6-4570-9f15-ac1c90ffefec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleGravity"",
                    ""type"": ""Button"",
                    ""id"": ""b2a21f4e-ce0a-4988-adf1-064ef02bbc10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3fd731b5-ee21-424e-8565-fe7e502797ea"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""438aa315-6581-4a16-9abc-8cd21cc3f509"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7ca58a52-0b0a-42f8-8309-85f232b046fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4c32be3f-389a-465f-9656-892c9e746218"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""23618fc6-f07c-4c30-b2db-d1405b02fae4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""190d08e4-fbe2-41e4-91ba-17654bbf9e91"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""0fb8f81a-7b65-4b5a-b087-2f32e35d0028"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""de9aa2bb-891b-4fd4-af4c-d795466a81ca"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""429cd0fb-8b4e-468b-863c-1cc452119039"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e5d0a5d-6eb6-44d7-b17b-bc0367e97713"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e814fbca-d6b4-4af1-8604-1a9e0c23ed81"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e7c0487f-d3e1-4a32-93fc-afba9c72d959"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""NewGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""413b651e-ff19-406d-8cdc-5e1d5959a2e4"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard;LeftKeyboard"",
                    ""action"": ""NewGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1515c8eb-96ff-43c2-896d-077e4ee8fc63"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""FloatUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4afd33cc-ceed-441b-b8b8-6eb8dec479aa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""FloatUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb775cd0-9562-4ddd-b0b7-1d86ee586e5e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""FloatUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e0486f7-f102-4d02-b266-e344db615c4d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""FloatDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbf74302-4536-4c5e-af9b-5afd35a023cb"",
                    ""path"": ""<Keyboard>/rightAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FloatDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea19d7a8-6b58-431a-a24a-32e907993f49"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""FloatDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18dbc261-c8fe-4001-b841-615419449f2a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""ToggleGravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""486e76c6-727b-418d-9922-75976fcd484a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ToggleGravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a29026e-d5d7-4fc0-811c-68b82f5d74aa"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""ToggleGravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31a23134-4bb4-4b33-a3b7-0414b702a954"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""ToggleLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7d7f434-d9d5-4753-bbcc-949f11ff9ec2"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""ToggleLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""244c45d1-2c94-4c1b-b3ae-bc8fb129ab5d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ToggleLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Join"",
            ""id"": ""27553537-7da2-4f41-a4c0-29fb2e70d838"",
            ""actions"": [
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""bae395c9-cb42-479f-a2e6-ea20d529d452"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24caba55-2565-44ae-8e8b-56cce5f92c16"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d513f3f-16fe-4b9f-9013-00dbde041799"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LeftKeyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c8820be-5471-4253-82c4-3dcc2f5fb0c9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RightKeyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""LeftKeyboard"",
            ""bindingGroup"": ""LeftKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""RightKeyboard"",
            ""bindingGroup"": ""RightKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_NewGame = m_Player.FindAction("NewGame", throwIfNotFound: true);
        // Ghost
        m_Ghost = asset.FindActionMap("Ghost", throwIfNotFound: true);
        m_Ghost_Move = m_Ghost.FindAction("Move", throwIfNotFound: true);
        m_Ghost_NewGame = m_Ghost.FindAction("NewGame", throwIfNotFound: true);
        m_Ghost_FloatUp = m_Ghost.FindAction("FloatUp", throwIfNotFound: true);
        m_Ghost_FloatDown = m_Ghost.FindAction("FloatDown", throwIfNotFound: true);
        m_Ghost_ToggleLight = m_Ghost.FindAction("ToggleLight", throwIfNotFound: true);
        m_Ghost_ToggleGravity = m_Ghost.FindAction("ToggleGravity", throwIfNotFound: true);
        // Join
        m_Join = asset.FindActionMap("Join", throwIfNotFound: true);
        m_Join_Join = m_Join.FindAction("Join", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_NewGame;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @NewGame => m_Wrapper.m_Player_NewGame;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @NewGame.started += instance.OnNewGame;
            @NewGame.performed += instance.OnNewGame;
            @NewGame.canceled += instance.OnNewGame;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @NewGame.started -= instance.OnNewGame;
            @NewGame.performed -= instance.OnNewGame;
            @NewGame.canceled -= instance.OnNewGame;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Ghost
    private readonly InputActionMap m_Ghost;
    private List<IGhostActions> m_GhostActionsCallbackInterfaces = new List<IGhostActions>();
    private readonly InputAction m_Ghost_Move;
    private readonly InputAction m_Ghost_NewGame;
    private readonly InputAction m_Ghost_FloatUp;
    private readonly InputAction m_Ghost_FloatDown;
    private readonly InputAction m_Ghost_ToggleLight;
    private readonly InputAction m_Ghost_ToggleGravity;
    public struct GhostActions
    {
        private @PlayerInputActions m_Wrapper;
        public GhostActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Ghost_Move;
        public InputAction @NewGame => m_Wrapper.m_Ghost_NewGame;
        public InputAction @FloatUp => m_Wrapper.m_Ghost_FloatUp;
        public InputAction @FloatDown => m_Wrapper.m_Ghost_FloatDown;
        public InputAction @ToggleLight => m_Wrapper.m_Ghost_ToggleLight;
        public InputAction @ToggleGravity => m_Wrapper.m_Ghost_ToggleGravity;
        public InputActionMap Get() { return m_Wrapper.m_Ghost; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GhostActions set) { return set.Get(); }
        public void AddCallbacks(IGhostActions instance)
        {
            if (instance == null || m_Wrapper.m_GhostActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GhostActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @NewGame.started += instance.OnNewGame;
            @NewGame.performed += instance.OnNewGame;
            @NewGame.canceled += instance.OnNewGame;
            @FloatUp.started += instance.OnFloatUp;
            @FloatUp.performed += instance.OnFloatUp;
            @FloatUp.canceled += instance.OnFloatUp;
            @FloatDown.started += instance.OnFloatDown;
            @FloatDown.performed += instance.OnFloatDown;
            @FloatDown.canceled += instance.OnFloatDown;
            @ToggleLight.started += instance.OnToggleLight;
            @ToggleLight.performed += instance.OnToggleLight;
            @ToggleLight.canceled += instance.OnToggleLight;
            @ToggleGravity.started += instance.OnToggleGravity;
            @ToggleGravity.performed += instance.OnToggleGravity;
            @ToggleGravity.canceled += instance.OnToggleGravity;
        }

        private void UnregisterCallbacks(IGhostActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @NewGame.started -= instance.OnNewGame;
            @NewGame.performed -= instance.OnNewGame;
            @NewGame.canceled -= instance.OnNewGame;
            @FloatUp.started -= instance.OnFloatUp;
            @FloatUp.performed -= instance.OnFloatUp;
            @FloatUp.canceled -= instance.OnFloatUp;
            @FloatDown.started -= instance.OnFloatDown;
            @FloatDown.performed -= instance.OnFloatDown;
            @FloatDown.canceled -= instance.OnFloatDown;
            @ToggleLight.started -= instance.OnToggleLight;
            @ToggleLight.performed -= instance.OnToggleLight;
            @ToggleLight.canceled -= instance.OnToggleLight;
            @ToggleGravity.started -= instance.OnToggleGravity;
            @ToggleGravity.performed -= instance.OnToggleGravity;
            @ToggleGravity.canceled -= instance.OnToggleGravity;
        }

        public void RemoveCallbacks(IGhostActions instance)
        {
            if (m_Wrapper.m_GhostActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGhostActions instance)
        {
            foreach (var item in m_Wrapper.m_GhostActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GhostActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GhostActions @Ghost => new GhostActions(this);

    // Join
    private readonly InputActionMap m_Join;
    private List<IJoinActions> m_JoinActionsCallbackInterfaces = new List<IJoinActions>();
    private readonly InputAction m_Join_Join;
    public struct JoinActions
    {
        private @PlayerInputActions m_Wrapper;
        public JoinActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Join => m_Wrapper.m_Join_Join;
        public InputActionMap Get() { return m_Wrapper.m_Join; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JoinActions set) { return set.Get(); }
        public void AddCallbacks(IJoinActions instance)
        {
            if (instance == null || m_Wrapper.m_JoinActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_JoinActionsCallbackInterfaces.Add(instance);
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
        }

        private void UnregisterCallbacks(IJoinActions instance)
        {
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
        }

        public void RemoveCallbacks(IJoinActions instance)
        {
            if (m_Wrapper.m_JoinActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IJoinActions instance)
        {
            foreach (var item in m_Wrapper.m_JoinActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_JoinActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public JoinActions @Join => new JoinActions(this);
    private int m_LeftKeyboardSchemeIndex = -1;
    public InputControlScheme LeftKeyboardScheme
    {
        get
        {
            if (m_LeftKeyboardSchemeIndex == -1) m_LeftKeyboardSchemeIndex = asset.FindControlSchemeIndex("LeftKeyboard");
            return asset.controlSchemes[m_LeftKeyboardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_RightKeyboardSchemeIndex = -1;
    public InputControlScheme RightKeyboardScheme
    {
        get
        {
            if (m_RightKeyboardSchemeIndex == -1) m_RightKeyboardSchemeIndex = asset.FindControlSchemeIndex("RightKeyboard");
            return asset.controlSchemes[m_RightKeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnNewGame(InputAction.CallbackContext context);
    }
    public interface IGhostActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnNewGame(InputAction.CallbackContext context);
        void OnFloatUp(InputAction.CallbackContext context);
        void OnFloatDown(InputAction.CallbackContext context);
        void OnToggleLight(InputAction.CallbackContext context);
        void OnToggleGravity(InputAction.CallbackContext context);
    }
    public interface IJoinActions
    {
        void OnJoin(InputAction.CallbackContext context);
    }
}
