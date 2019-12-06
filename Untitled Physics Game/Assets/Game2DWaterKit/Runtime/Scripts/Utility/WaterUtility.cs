namespace Game2DWaterKit.Utils
{
    using UnityEngine;

    internal static class WaterUtility
    {
        private static SimpleFixedSizeList<Vector2> _outputPoints = new SimpleFixedSizeList<Vector2>(8);
        internal static bool ClipPointsAgainstAABBEdge(SimpleFixedSizeList<Vector2> points, bool isBottomOrTopEdge, bool isBottomOrLeftEdge, float edgePosition)
        {
            int inputPointsCount = points.Count;

            if (inputPointsCount < 1)
                return false;

            _outputPoints.Clear();

            int coord = isBottomOrTopEdge ? 1 : 0; // 1->y : 0->x

            Vector2 previousPoint = points[inputPointsCount - 1];
            bool isPreviousPointInside = isBottomOrLeftEdge ? (previousPoint[coord] > edgePosition) : (previousPoint[coord] < edgePosition);

            bool areInputPointsUnchanged = isPreviousPointInside;

            for (int i = 0; i < inputPointsCount; i++)
            {
                Vector2 currentPoint = points[i];
                bool isCurrentPointInside = isBottomOrLeftEdge ? (currentPoint[coord] > edgePosition) : (currentPoint[coord] < edgePosition);

                if (isCurrentPointInside != isPreviousPointInside)
                {
                    //intersection
                    Vector2 dir = currentPoint - previousPoint;
                    float x = !isBottomOrTopEdge ? edgePosition : previousPoint.x + (dir.x / dir.y) * (edgePosition - previousPoint.y);
                    float y = isBottomOrTopEdge ? edgePosition : previousPoint.y + (dir.y / dir.x) * (edgePosition - previousPoint.x);
                    _outputPoints.Add(new Vector2(x, y));

                    areInputPointsUnchanged = false;
                }

                if (isCurrentPointInside)
                    _outputPoints.Add(currentPoint);

                previousPoint = currentPoint;
                isPreviousPointInside = isCurrentPointInside;
            }

            points.CopyFrom(_outputPoints);

            return areInputPointsUnchanged;
        }

        internal static float Min(float a, float b, float c, float d)
        {
            float m1 = a < b ? a : b;
            float m2 = c < d ? c : d;
            return m1 < m2 ? m1 : m2;
        }

        internal static float Max(float a, float b, float c, float d)
        {
            float m1 = a > b ? a : b;
            float m2 = c > d ? c : d;
            return m1 > m2 ? m1 : m2;
        }

        internal static void SafeDestroyObject(Object target)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Object.DestroyImmediate(target);
                return;
            }
#endif
            Object.Destroy(target);
        }
    }

    internal class SimpleFixedSizeList<T>
    {
        private T[] _elements;
        private int _count;

        internal SimpleFixedSizeList(int size)
        {
            _elements = new T[size];
            _count = 0;
        }
        
        internal int Count { get { return _count; } }

        internal T this[int index]
        {
            get
            {
                //if (index < 0 || index > _count - 1)
                //    Debug.LogError("Index is out of range!");

                return _elements[index];
            }
        }

        internal void Add(T point)
        {
            //if (_count == _points.Length)
            //    Debug.LogError("Max size reached!");

            _elements[_count] = point;
            _count++;
        }

        internal void Clear()
        {
            _count = 0;
        }

        internal void CopyFrom(SimpleFixedSizeList<T> points)
        {
            //if (points._count > _count)
            //    Debug.LogError("Source array is larger than destination!");

            _count = points._count;
            for (int i = 0; i < _count; i++)
            {
                _elements[i] = points[i];
            }
        }
    }
}
