namespace Game.Enemy.Factories
{
    public class Default : Base
    {
        private const float MaxZPosition = -5;

        protected override void Create()
        {
            var factoryPosition = _transform.position;
            var factoryForward = _transform.forward;

            InstantiateEnemy(factoryPosition, _transform.parent).StartMove(_ =>
                factoryPosition + factoryForward * (factoryPosition.z - MaxZPosition));
        }
    }
}