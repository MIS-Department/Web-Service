using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OG_MFTG.DataLayer.Repositories;

namespace OG_MFTG.IntergrationTest
{
    [TestFixture]
    public class TimeCategoryTest
    {
        [Test]
        public void SelectAllTest()
        {
            var repo = new TimeCategoryRepository();
            var result = repo.SelectAll();

            Assert.AreEqual(4, result.Result.Count());
        }
    }
}
