using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    public InputEvent<Vector2> OnMove;
    public InputEvent<Vector2> OnLook;
    public InputEvent OnDash;

    public InputActionAsset inputAsset;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        OnMove.Init();
        OnLook.Init();
        OnDash.Init();
    }

    private void Start()
    {
        foreach (var map in inputAsset.actionMaps)
        {
            foreach (var action in map.actions)
            {
            }
        }
    }
}
