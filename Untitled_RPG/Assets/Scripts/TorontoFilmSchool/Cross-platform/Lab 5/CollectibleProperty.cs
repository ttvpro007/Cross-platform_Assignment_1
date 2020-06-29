using RPG.Control;
using UnityEngine;

public class CollectibleProperty : MonoBehaviour, IRaycastable
{
    [SerializeField] public enum CollectibleType
    {
        MovementSpeedBoost, GodModeBoost
    }

    public CollectibleType collectibleType;

    public CursorType GetCursorType()
    {
        return CursorType.Pickup;
    }

    public bool HandleRaycast(PlayerController collingController)
    {
        return true;
    }
}