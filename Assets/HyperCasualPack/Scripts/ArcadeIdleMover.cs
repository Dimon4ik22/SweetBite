using System;
using HyperCasualPack.ScriptableObjects;
using HyperCasualPack.ScriptableObjects.Channels;
using UnityEngine;

namespace HyperCasualPack
{
    public enum ArcadeIdleMoverType{PLAYER,EMPLOYEE}
    public class ArcadeIdleMover : MonoBehaviour
    {
        [SerializeField] Rigidbody _rbd;
        [SerializeField] MovementDataSO _movementDataSO;
        [SerializeField] InputChannelSO _inputChannelSo;
        public ArcadeIdleMoverType ArcadeIdleMoverType;
        Vector3 _currentInputVector;

        public bool IsStopped => _currentInputVector.sqrMagnitude < 0.3f;

        void FixedUpdate()
        {
            switch (ArcadeIdleMoverType)
            {
                case ArcadeIdleMoverType.PLAYER:
                    _rbd.velocity = _currentInputVector;
                    break;
                case ArcadeIdleMoverType.EMPLOYEE:
                    break;
            }
        }

        void OnEnable()
        {
            _inputChannelSo.JoystickUpdate += OnJoystickUpdate;
        }

        void OnDisable()
        {
            _inputChannelSo.JoystickUpdate -= OnJoystickUpdate;
        }

        void OnJoystickUpdate(Vector2 newMoveDirection)
        {
            switch (ArcadeIdleMoverType)
            {
                case ArcadeIdleMoverType.PLAYER:
                    if (newMoveDirection.magnitude >= 1f)
                    {
                        newMoveDirection.Normalize();
                    }

                    _currentInputVector = new Vector3(newMoveDirection.x * _movementDataSO.SideMoveSpeed, _rbd.velocity.y, newMoveDirection.y * _movementDataSO.ForwardMoveSpeed);

                    Vector3 lookDir = new Vector3(_currentInputVector.x, 0f, _currentInputVector.z);
                    if (lookDir != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
                    }
                    break;
                case ArcadeIdleMoverType.EMPLOYEE:
                    break;
            }

        }
    }
}