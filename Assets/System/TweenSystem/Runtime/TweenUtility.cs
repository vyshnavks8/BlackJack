using UnityEngine;

namespace RedDevil.Tween
{
    public static class TweenUtility
    {
        public static float GetDirection(RotationDirection dir)
        {
            return dir switch
            {
                RotationDirection.ClockWise => 1,
                RotationDirection.AntiClockWise => -1,
                _ => 1
            };
        }

        public static Vector3 GetAxis(RotationAxis axis)
        {
            return axis switch
            {
                RotationAxis.X => new Vector3(1, 0, 0),
                RotationAxis.Y => new Vector3(0, 1, 0),
                RotationAxis.Z => new Vector3(0, 0, 1),
                RotationAxis.XY => new Vector3(1, 1, 0),
                RotationAxis.XZ => new Vector3(1, 0, 1),
                RotationAxis.YZ => new Vector3(0, 1, 1),
                RotationAxis.XYZ => new Vector3(1, 1, 1),
                _ => new Vector3(1, 1, 1)
            };
        }
    }
}