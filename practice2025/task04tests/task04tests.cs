using Xunit;
using task04;

namespace task04tests
{
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectStats()
        {
            ISpaceship fighter = new Fighter();
            Assert.Equal(100, fighter.Speed);
            Assert.Equal(50, fighter.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void FighterFirePower_ShouldBeWeakerThanCruiserFirePower()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.FirePower < cruiser.FirePower);
        }

        [Fact]
        public void Fighter_MovesForvardCorrect()
        {
            var fighter = new Fighter();
            int start = fighter.Position;
            fighter.MoveForward();
            Assert.Equal(start + fighter.Speed, fighter.Position);
            Assert.Equal(100, fighter.Position);
            fighter.MoveForward();
            Assert.Equal(200, fighter.Position);
            fighter.MoveForward();
            Assert.Equal(300, fighter.Position);
        }

        [Fact]
        public void Cruiser_MovesForvardCorrect()
        {
            var cruiser = new Cruiser();
            int start = cruiser.Position;
            cruiser.MoveForward();
            Assert.Equal(start + cruiser.Speed, cruiser.Position);
            Assert.Equal(50, cruiser.Position);
            cruiser.MoveForward();
            Assert.Equal(100, cruiser.Position);
            cruiser.MoveForward();
            Assert.Equal(150, cruiser.Position);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectRotation()
        {
            Fighter fighter = new Fighter();
            fighter.Rotate(150);
            Assert.Equal(150, fighter.Angle);
            fighter.Rotate(300);
            Assert.Equal(90, fighter.Angle);
            fighter.Rotate(-500);
            Assert.Equal(310, fighter.Angle);
        }

        [Fact]
        public void Cruiser_ShouldHaveCorrectRotation()
        {
            var cruiser = new Cruiser();
            cruiser.Rotate(90);
            Assert.Equal(90, cruiser.Angle);
            cruiser.Rotate(500);
            Assert.Equal(230, cruiser.Angle);
            cruiser.Rotate(-300);
            Assert.Equal(290, cruiser.Angle);
            cruiser.Rotate(-1000);
            Assert.Equal(10, cruiser.Angle);
            cruiser.Rotate(720);
            Assert.Equal(10, cruiser.Angle);
        }

        [Fact]
        public void Fighter_FiresCorrectly()
        {
            var fighter = new Fighter();
            Assert.Equal(100, fighter.MissilCount);
            fighter.Fire();
            Assert.Equal(99, fighter.MissilCount);
        }

        [Fact]
        public void Cruiser_FiresCorrectly()
        {
            var cruiser = new Cruiser();
            Assert.Equal(100, cruiser.MissilCount);
            cruiser.Fire();
            Assert.Equal(99, cruiser.MissilCount);
            cruiser.Fire();
            Assert.Equal(98, cruiser.MissilCount);
        }

    }
}
