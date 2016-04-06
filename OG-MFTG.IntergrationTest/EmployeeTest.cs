﻿using System;
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
    }
}
