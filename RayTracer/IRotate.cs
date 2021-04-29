using PathTracer.Geometry;

namespace PathTracer
{
    /// <summary>
    /// Представляет вращаемые объекты
    /// </summary>
    public interface IRotate
    {
        /// <summary>
        /// Вращает объект на заданное число радиан вокргу оси axis
        /// </summary>
        /// <param name="axis">ось вокруг которой происходит вращение</param>
        /// <param name="angle">угол в радианах</param>
        void Rotate(Vector3 axis, float angle);
    }
}
