using FormulaApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaApp.UnitTests.Fixtures
{
    public class FansFixtures
    {
        public static List<Fan> GetFans() => new()
        {
            new Fan()
            {
                Email = "madushika@test.com",
                Id = 1,
                Name = "Madushika"
            },
            new Fan()
            {
                Email = "nuwan@test.com",
                Id = 1,
                Name = "Nuwan"
            }
        };
    }
}
