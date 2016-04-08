using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OG_MFTG.DataLayer.Repositories;

namespace OG_MFTG.IntergrationTest
{
    [TestFixture]
    class EmployeeTest
    {
        [Test]
        public async Task SelectAllTest()
        {
            var repo = new EmployeeRepository();

            var result = await repo.SelectAll();

            foreach (var model in result)
            {
                Console.WriteLine("EmployeeId: {0}", model.EmployeeId);
                Console.WriteLine("FullName: {0} {1} {2}", model.FirstName,model.MiddleName, model.LastName);
                Console.WriteLine("Employee Number: {0}", model.EmployeeNumber);
                Console.WriteLine("ImageEmployee {0}", model.ImageEmployee);
                Console.WriteLine("===================================");
            }

        }

        [Test]
        public async Task CheckNotification()
        {
            var repo  = new DailyTimeRecordRepository();

            Console.WriteLine(await repo.GetEmplopyeeNotification(911));

        }

        [Test]
        public async Task SelectByIdTest()
        {
            var repo = new EmployeeRepository();

            var result = await repo.SelectById(851);

            Console.WriteLine("{0}", result.LastName);

        }

        //[Test]
        //public async Task SelectByIdSample()
        //{
        //    var repo = new EmployeeRepository();
        //    var result = await repo.SelectByIdSample(851);
        //    Console.WriteLine("{0}",result.LastName);
        //} 
    }
}
