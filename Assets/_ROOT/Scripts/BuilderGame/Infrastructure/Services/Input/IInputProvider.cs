namespace BuilderGame.Infrastructure.Services.Input
{
    using UnityEngine;

    public interface IInputProvider
    {
        Vector2 Axis { get; }
    }
}