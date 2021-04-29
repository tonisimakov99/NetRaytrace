using RayTracer.Geometry;

namespace RayTracer
{
    /// <summary>
    /// Представляет объекты которые можно двигать
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Двигает объект на заданную delta
        /// </summary>
        /// <param name="delta">Изменение позиции объекта</param>
        void Move(Vector3 delta);
    }
}
