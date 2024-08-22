using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace RedDevil.Tween
{
    public enum RotationAxis
    {
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        XYZ,
    }

    public enum RotationDirection
    {
        ClockWise,
        AntiClockWise
    }  

    public class RotationTween : MonoBehaviour
    {
        [SerializeField] private bool startOnAwake;   
        [SerializeField] private bool infinite;
        [SerializeField] private RotationAxis rotationAxis;
        [SerializeField] private RotationDirection rotationDirection;
    
        [SerializeField, Range(0, 360)] private float angle = 90;
        [SerializeField] private float duration = 3;
        [SerializeField] private Ease ease = Ease.Linear;

        [Header("Align Angle"),Tooltip("Will only work for specific axis")] [SerializeField, Range(0, 360)]
        private float alignAngle;
        [SerializeField] private UnityEvent OnSuccess;
        [SerializeField] private UnityEvent OnFail;
        [SerializeField] private UnityEvent OnStartRotation;
        [SerializeField] private UnityEvent OnCompleteRotation;
        private Vector3 nextAngle;
        private Vector3 localRotation;

        private void Awake()
        {
            localRotation = transform.localEulerAngles;
            if (startOnAwake)
            {
                Rotate();
            }
        }

        public void Rotate()
        {
            OnStartRotation?.Invoke();
            if (infinite)
            {
                
                nextAngle = SetNextAngle(360);
                transform.DOLocalRotate(nextAngle, duration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(ease);
                return;
            }
            nextAngle = SetNextAngle(angle);
            transform.DOLocalRotate(nextAngle, duration, RotateMode.LocalAxisAdd).SetEase(ease)
                .OnComplete(CheckAligned);
        }

        public void RotateOpposite()
        {
            OnStartRotation?.Invoke();
            if (infinite)
            {
                nextAngle = SetNextAngle(360);
                transform.DOLocalRotate(-nextAngle, duration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(ease);
                return;
            }
            nextAngle = SetNextAngle(angle);
            transform.DOLocalRotate(-nextAngle, duration, RotateMode.LocalAxisAdd).SetEase(ease)
                .OnComplete(CheckAligned);
        } 
        public void RotateFlip()
        {
            OnStartRotation?.Invoke();
            if (infinite)
            {
                rotationDirection = FlipDirection(rotationDirection);
                nextAngle = SetNextAngle(360);
                transform.DOLocalRotate(nextAngle, duration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(ease);
            }
          
        }

        private RotationDirection FlipDirection(RotationDirection direction)
        {
            return direction switch
            {
                RotationDirection.ClockWise => RotationDirection.AntiClockWise,
                RotationDirection.AntiClockWise => RotationDirection.ClockWise,
                _ => RotationDirection.ClockWise
            };
        }

        public void Rotate(float newAngle)
        {
            OnStartRotation?.Invoke();
            nextAngle = SetNextAngle(newAngle);
            transform.DOLocalRotate(nextAngle, duration, RotateMode.LocalAxisAdd).SetEase(ease)
                .OnComplete(CheckAligned);
        }

        public void RotateOpposite(float newAngle)
        {
            OnStartRotation?.Invoke();
            nextAngle = SetNextAngle(newAngle);
            transform.DOLocalRotate(-nextAngle, duration, RotateMode.LocalAxisAdd).SetEase(ease)
                .OnComplete(CheckAligned);
        }

        public void ResetRotation()
        {
            OnStartRotation?.Invoke();
            transform.DOLocalRotate(localRotation, duration).SetEase(ease).OnComplete(OnReset);
        }

        private void CheckAligned()
        {
            float ang=0;
            switch (rotationAxis)
            {
                case RotationAxis.X:
                    ang = transform.localEulerAngles.x;
                    break;
                case RotationAxis.Y:
                    ang = transform.localEulerAngles.y;
                    break;
                case RotationAxis.Z:
                    ang = transform.localEulerAngles.z;
                    break;
                case RotationAxis.XY:
                    break;
                case RotationAxis.XZ:
                    break;
                case RotationAxis.YZ:
                    break;
                case RotationAxis.XYZ:
                    break;
            }
          
          
            if (Math.Abs(ang - alignAngle) < 0.1f)
            {
                OnSuccess?.Invoke();
            }
            else
            {
                OnFail?.Invoke();
            }
            OnCompleteRotation?.Invoke();
        }

        private void OnReset()
        {
            OnFail?.Invoke();
        }

        public void ChangeDirection(RotationDirection direction)
        {
            rotationDirection = direction;
        }

        private Vector3 SetNextAngle(float addedAngle)
        {
            return TweenUtility.GetAxis(rotationAxis) * (TweenUtility.GetDirection(rotationDirection) * addedAngle);
        }
    }
}