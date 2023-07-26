using UnityEngine;
using EricGames.Core.Equipment;
using EricGames.Core.Characters;

namespace My2DGame.Characters.Enemies
{
    [RequireComponent(typeof(Character2D))]
    public class StoneStone : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;

        enum MonsterAIState
        {
            IDLE,
            MOVE,
            ATTACK,
        }

        MonsterAIState aiState;
        [SerializeField] private Weapon backWeapon;
        [SerializeField] private Weapon frontWeapon;

        void Update()
        {
        }
    }
}
