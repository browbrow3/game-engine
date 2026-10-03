using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Graphics;
using GameEngine.Graphics.Console;
using GameEngine.Graphics.Console.Sprites;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using GameEngine_Tests.Helpers;
using Moq;

namespace GameEngine_Tests.Graphics
{
    public class GraphicsEngineTests : IAsyncLifetime
    {
        private readonly Mock<IMap> _mockMap = new(MockBehavior.Strict);
        private readonly ConsoleProviderStub _consoleProviderStub = new ConsoleProviderStub();
        private readonly TileDefinition _stone = new TileDefinition
        {
            Type = TileType.Stone,
            Name = "Stone",
            Tangiable = false,
            SpeedMultiplier = 1.0f,
            StatusEffects = 0,
            PrintChar = ' '
        };
        private readonly TileDefinition _stoneWall = new TileDefinition
        {
            Type = TileType.StoneWall,
            Name = "Stone Wall",
            Tangiable = true,
            SpeedMultiplier = 0.0f,
            StatusEffects = 0,
            PrintChar = '#'
        };
        private readonly TileDefinition _water = new TileDefinition
        {
            Type = TileType.Water,
            Name = "Water",
            Tangiable = false,
            SpeedMultiplier = 0.5f,
            StatusEffects = 0,
            PrintChar = '~'
        };

        private readonly GraphicsEngine graphicsEngine;

        public GraphicsEngineTests()
        {
            this.graphicsEngine = new GraphicsEngine(
                this._consoleProviderStub,
                this._mockMap.Object,
                3,
                3,
                1
            );
        }

        public async Task InitializeAsync() { }

        public async Task DisposeAsync()
        {
            await this.graphicsEngine.Stop();
        }

        #region Render tests
        [Fact]
        public void Render_NoEntities_RendersCorrectly()
        {
            this._mockMap.Setup(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });

            this.graphicsEngine.Render();

            this._consoleProviderStub.GetOutput().Should().BeEquivalentTo([
                "<CLEAR>",
                "#~~\n#  \n#  \n"
            ]);

            this._mockMap.Verify(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny), Times.Once);
        }

        [Fact]
        public void RenderWithEntities_RendersCorrectly()
        {
            Mock<IEntity> mockEntity1 = new(MockBehavior.Strict);
            Mock<IEntity> mockEntity2 = new(MockBehavior.Strict);
            Mock<IEntity> mockEntity3 = new(MockBehavior.Strict);

            mockEntity1.Setup(e => e.Sprite.GetFrame()).Returns(new Frame('1'));
            mockEntity3.Setup(e => e.Sprite.GetFrame()).Returns(new Frame('3'));

            this._mockMap.Setup(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone, new Dictionary<int, IEntity> { { 0, mockEntity1.Object }, { 1, mockEntity2.Object } }); });
            this._mockMap.Setup(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone, new Dictionary<int, IEntity> { { 2, mockEntity3.Object } }); });

            this.graphicsEngine.Render();

            this._consoleProviderStub.GetOutput().Should().BeEquivalentTo([
                "<CLEAR>",
                "#~~\n#1 \n# 3\n"
            ]);

            this._mockMap.Verify(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            mockEntity1.Verify(e => e.Sprite.GetFrame(), Times.Once);
            mockEntity3.Verify(e => e.Sprite.GetFrame(), Times.Once);
        }

        [Fact]
        public void Render_FrameLargerThenMap_RendersCorrectlyWithBlankSpaces()
        {
            this._mockMap.Setup(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(3, 0, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(4, 0, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(3, 1, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(4, 1, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(3, 2, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(4, 2, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(0, 3, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(1, 3, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(2, 3, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(3, 3, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(4, 3, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(0, 4, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(1, 4, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(2, 4, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(3, 4, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });
            this._mockMap.Setup(m => m.TryGetTile(4, 4, out It.Ref<ITile?>.IsAny)).Returns(false).Callback((int x, int y, out ITile? tile) => { tile = null; });

            GraphicsEngine engine = new GraphicsEngine(
                this._consoleProviderStub,
                this._mockMap.Object,
                5,
                5,
                1
            );

            engine.Render();

            this._consoleProviderStub.GetOutput().Should().BeEquivalentTo([
                "<CLEAR>",
                "#~~??\n#  ??\n#  ??\n?????\n?????\n"
            ]);

            this._mockMap.Verify(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(3, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(4, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(3, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(4, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(3, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(4, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 3, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 3, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 3, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(3, 3, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(4, 3, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 4, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 4, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 4, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(3, 4, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(4, 4, out It.Ref<ITile?>.IsAny), Times.Once);
        }
        #endregion

        #region Start/Stop tests

        [Fact]
        public async Task Stop_BeforeStart_ReturnsCompletedTask()
        {
            this.graphicsEngine.Stop().Should().Be(Task.CompletedTask);
        }

        [Fact]
        public async Task Stop_ShouldStopInputLoop()
        {
            this._mockMap.Setup(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });

            this.graphicsEngine.Start();

            Thread.Sleep(100);

            await this.graphicsEngine.Stop();
            
            Thread.Sleep(5000);

            this._consoleProviderStub.GetOutput().Should().BeEquivalentTo([
                "<CLEAR>",
                "#~~\n#  \n#  \n",
            ]);

            this._mockMap.Verify(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny), Times.Once);
            this._mockMap.Verify(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny), Times.Once);
        }

        [Fact]
        public async Task Start_StartedTwice_ThrowsInvalidOperationException()
        {
            this._mockMap.Setup(m => m.TryGetTile(It.IsAny<int>(), It.IsAny<int>(), out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });

            this.graphicsEngine.Start();
            this.graphicsEngine.Invoking(x => x.Start()).Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void Start_RendersCorrectlyInLoop_Stop_StopsLoop()
        {
            GraphicsEngine engine = new GraphicsEngine(
                this._consoleProviderStub,
                this._mockMap.Object,
                3,
                3,
                1
            );

            this._mockMap.Setup(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._water); });
            this._mockMap.Setup(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stoneWall); });
            this._mockMap.Setup(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });
            this._mockMap.Setup(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny)).Returns(true).Callback((int x, int y, out ITile? tile) => { tile = new Tile(this._stone); });

            engine.Start();

            Thread.Sleep(3000);

            engine.Stop();

            this._consoleProviderStub.GetOutput().Should().BeEquivalentTo([
                "<CLEAR>",
                "#~~\n#  \n#  \n",
                "<CLEAR>",
                "#~~\n#  \n#  \n",
                "<CLEAR>",
                "#~~\n#  \n#  \n",
            ]);

            this._mockMap.Verify(m => m.TryGetTile(0, 0, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(1, 0, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(2, 0, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(0, 1, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(1, 1, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(2, 1, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(0, 2, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(1, 2, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
            this._mockMap.Verify(m => m.TryGetTile(2, 2, out It.Ref<ITile?>.IsAny), Times.Exactly(3));
        }

        #endregion
    }
}
