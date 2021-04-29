using PathTracer.Geometry;

namespace PathTracer
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
