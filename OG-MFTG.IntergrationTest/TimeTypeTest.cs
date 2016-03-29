using HR_Department.Models.Tables;
using NUnit.Framework;
using OG_MFTG.DataLayer.Repositories;

namespace OG_MFTG.IntergrationTest
{
    [TestFixture]
    public class TimeTypeTest
    {
        [Test]
        public void TimeTypeInsertTest()
        {
            var repo = new TimeTypeRepository();

            var model = new TimeType
            {
                Name = "HRD"
            };

            var result = repo.Insert(model);

            Assert.AreEqual(3, result.Result);
        }
    }
}
