namespace BuilderGame.Infrastructure.Services.Input
{
    using UnityEngine;

    public class InputProvider : IInputProvider
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 Axis => ReadInput();

        private Vector2 ReadInput() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}