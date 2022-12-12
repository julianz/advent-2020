
namespace Advent.Util.Tests {
    [TestClass]
    public class CoordsTests {
        [TestMethod]
        public void CoordAdditionTests() {
            var a = new Coords(1, 4);
            var b = new Coords(2, 5);

            Assert.AreEqual(new Coords(3, 9), a + b);
        }
    }
}