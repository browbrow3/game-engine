using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Graphics.Console.Sprites;

namespace GameEngine_Tests.Entities
{
    public class EntityTests
    {
        #region HandleEvent Tests
        [Fact]
        public void HandleEvent_NotImplemented_ThrowsNotImplementedException() // note that this is not currently implented so will throw. this test will need to be updated once we implement the HandleEvent method
        {
            Entity entity = new Entity()
            {
                Sprite = new Sprite('x')
            };

            IEvent e = new Event(EventType.Input);

            entity.Invoking(x => x.HandleEvent(e)).Should().ThrowExactly<NotImplementedException>();

        }
        #endregion
    }
}
