namespace BuilderGame.Infrastructure.Input
{
    using UnityEngine;

    public interface IInputProvider
    {
        Vector2 Axis { get; }
    }
}