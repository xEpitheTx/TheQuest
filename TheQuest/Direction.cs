using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }
    public static class Extension
    {
        public static Direction ClockwiseDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                default:
                    throw new NotImplementedException($"Direction {direction} not implemented");
            }
        }

        public static Direction CounterClockwiseDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Left;
                case Direction.Right:
                    return Direction.Up;
                case Direction.Down:
                    return Direction.Right;
                case Direction.Left:
                    return Direction.Down;
                default:
                    throw new NotImplementedException($"Direction {direction} is not implemented");
            }
        }

        public static Direction InverseDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new NotImplementedException($"Direction {direction} is not implemented");
            }
        }
    }
}
