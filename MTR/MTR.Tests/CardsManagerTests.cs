using Autofac.Extras.Moq;

using MTR.Core;
using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Tests
{
    public class CardsManagerTests
    {
        [Fact]
        public void GetNextRoundSuit_NoPreviousSuit_ReturnsSpades()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new());

            Assert.Equal(Suit.SPADES, result);
        }

        [Fact]
        public void GetNextRoundSuit_PreviousSuitSpades_ReturnsHearts()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new() { new Round { Suit = Suit.SPADES } });

            Assert.Equal(Suit.HEARTS, result);
        }

        [Fact]
        public void GetNextRoundSuit_PreviousSuitClubs_ReturnsHearts()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new() { new Round { Suit = Suit.CLUBS } });

            Assert.Equal(Suit.SPADES, result);
        }
    }
}