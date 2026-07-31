using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Map.Tiles;
using Moq;
using System.Text.Json;

namespace GameEngine_Tests.Map.Tiles
{
    public class TileTests
    {
        private TileDefinition definition = new TileDefinition()
        {
            Type = TileType.Grass,
            Name = "Grass",
            Tangiable = false,
            SpeedMultiplier = 4.0f,
            StatusEffects = 3,
            PrintChar = ','
        };

        #region Constructor Tests
        [Fact]
        public void Constructor_CanConstructFromDefinition()
        {
            Tile tile = new Tile(definition);

            tile.Type.Should().Be(TileType.Grass);
            tile.Name.Should().Be("Grass");
            tile.Tangiable.Should().BeFalse();
            tile.SpeedMultiplier.Should().Be(4.0f);
            tile.StatusEffects.Should().Be(3);
            tile.PrintChar.Should().Be(',');
            tile.Passable.Should().BeTrue(); // no entities, definition is not tangiable.
        }
        #endregion

        #region Passable Tests
        [Fact]
        public void Passable_TileIsNotTangiableAndNoEntitiesPresent_ReturnsTrue()
        {
            Tile tile = new Tile(definition);

            tile.Passable.Should().BeTrue(); // no entities, definition is not tangiable.
        }

        [Fact]
        public void Passable_TileIsTangiable_ReturnsFalse()
        {
            TileDefinition def = new TileDefinition()
            {
                Type = TileType.Grass,
                Name = "Grass",
                Tangiable = true,
                SpeedMultiplier = 4.0f,
                StatusEffects = 3,
                PrintChar = ','
            };

            Tile tile = new Tile(def);

            tile.Passable.Should().BeFalse(); // no entities, definition is tangiable.
        }

        [Fact]
        public void Passable_EntityPresent_ReturnsFalse()
        {
            Tile tile = new Tile(
                definition,
                new Dictionary<int, IEntity>() 
                { 
                    { 1, new Entity() } 
                }
            );

            tile.Passable.Should().BeFalse(); // entities present, definition is not tangiable.
        }
        #endregion

        #region AddEntity Tests
        [Fact]
        public void AddEntity_EntityNotPresent_AddsEntityReturnsSuccess()
        {
            Tile tile = new Tile(definition);
            IEntity entity = new Entity();
            short result = tile.AddEntity(entity);
            result.Should().Be(0); // success
            tile.GetEntities().Should().Contain(entity);
        }

        [Fact]
        public void AddEntity_EntityIsAlreadyPresent_DoesNotAddEntity_ReturnsNonSuccess()
        {
            Tile tile = new Tile(definition);
            IEntity entity = new Entity();
            short result = tile.AddEntity(entity);
            result.Should().Be(0); // success
            tile.GetEntities().Should().Contain(entity);
        }
        #endregion

        #region RemoveEntity Tests
        [Fact]
        public void RemoveEntity_EntityIsPresent_RemovesEntity_ReturnsSuccess()
        {
            IEntity entity = new Entity();
            Tile tile = new Tile(
                definition,
                new Dictionary<int, IEntity>()
                {
                    { 0, entity }
                }
            );
            short result = tile.RemoveEntity(entity);
            result.Should().Be(0); // success
            tile.GetEntities().Should().NotContain(entity);
        }

        [Fact]
        public void RemoveEntity_EntityIsNotPresent_DoesNotRemoveEntity_ReturnsNonSuccess()
        {
            IEntity entity = new Entity();
            Tile tile = new Tile(definition);
            short result = tile.RemoveEntity(entity);
            result.Should().Be(-1); // non-success
            tile.GetEntities().Should().NotContain(entity);
        }
        #endregion

        #region GetEntities Tests
        [Fact]
        public void GetEntities_ReturnsAllEntitiesOnTile()
        {
            IEntity entity1 = new Entity();
            IEntity entity2 = new Entity();
            Tile tile = new Tile(
                definition,
                new Dictionary<int, IEntity>()
                {
                    { 1, entity1 },
                    { 2, entity2 }
                }
            );
            IEnumerable<IEntity> entities = tile.GetEntities();
            entities.Should().Contain(entity1);
            entities.Should().Contain(entity2);
        }
        #endregion

        #region PublishEvent Tests
        [Fact]
        public void PublishEvent_EntitiesPresent_InvokesHandleEventOnAllEntities()
        {
            Mock<IEntity> mockEntity1 = new Mock<IEntity>();
            Mock<IEntity> mockEntity2 = new Mock<IEntity>();

            IEntity entity1 = mockEntity1.Object;
            IEntity entity2 = mockEntity2.Object;

            mockEntity1.Setup(e => e.HandleEvent(It.IsAny<IEvent>()));
            mockEntity1.Setup(e => e.HandleEvent(It.IsAny<IEvent>()));

            Tile tile = new Tile(
                definition,
                new Dictionary<int, IEntity>()
                {
                    { 1, entity1 },
                    { 2, entity2 }
                }
            );
            IEvent testEvent = new Event(EventType.Collision);
            tile.PublishEvent(testEvent).Should().Be(0); // success
            Mock.Get(entity1).Verify(e => e.HandleEvent(testEvent), Times.Once);
            Mock.Get(entity2).Verify(e => e.HandleEvent(testEvent), Times.Once);
        }
        #endregion
    }
}
