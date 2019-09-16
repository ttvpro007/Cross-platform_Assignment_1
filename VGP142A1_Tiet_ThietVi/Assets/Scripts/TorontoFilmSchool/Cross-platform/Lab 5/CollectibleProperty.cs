using UnityEngine;

public class CollectibleProperty : MonoBehaviour
{
    [SerializeField] public enum CollectibleType
    {
        MovementSpeedBoost, GodModeBoost
    }

    public CollectibleType collectibleType;
}