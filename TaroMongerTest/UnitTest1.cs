using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TaroMongerCore;

namespace TaroMongerTest
{
    
    [TestClass]
    public class UnitTest1
    {
        private RateBook BuildRateBook(string planId, string state)
        {
            return new RateBook()
            {
                EffectiveDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                PlanId = planId,
                State = state
            };
        }

        private RatePage BuildRatePage(string name, int number)
        {
            return new RatePage
            {
                Name = name,
                Number = number
            };
        }

        private RateSection BuildRateSection(string name, int number)
        {
            return new RateSection()
            {
                Name = name,
                Number = number
            };
        }

        private RateItem GetCoverageA()
        {
            return new RateItem()
            {
                Name = "Coverage A",
                Id = Guid.NewGuid(),
                Type = "DropDown",
                Limit = 0,
                Hur = 0,
                Nhr = 0,
                Value = "100000",
                HurRated = false,
                LimitRated = false,
                NhrRated = false,
                Options = new List<RateOption>
                {
                    new RateOption()
                    {
                        Id = Guid.NewGuid(),
                        Value = "100000",
                        LimitFactor = 0,
                        LimitFlatAmount = 100000,
                        HurFactor = 0,
                        HurFlatAmount = 200,
                        NhrFactor = 0,
                        NhrFlatAmount = 250,
                        LimitBasedOff = new List<Guid>(),
                        HurBasedOff = new List<Guid>(),
                        NhrBasedOff = new List<Guid>(),
                        Selected = true
                    },
                    new RateOption()
                    {
                        Id = Guid.NewGuid(),
                        Value = "101000",
                        LimitFactor = 0,
                        LimitFlatAmount = 101000,
                        HurFactor = 0,
                        HurFlatAmount = 210,
                        NhrFactor = 0,
                        NhrFlatAmount = 275,
                        LimitBasedOff = new List<Guid>(),
                        HurBasedOff = new List<Guid>(),
                        NhrBasedOff = new List<Guid>(),
                        Selected = false
                    }
                }
            };
        }

        private RateItem GetCoverageB(RateItem coverageA)
        {
            return new RateItem()
            {
                Name = "Coverage B",
                Id = Guid.NewGuid(),
                Type = "DropDown",
                Limit = 0,
                Hur = 0,
                Nhr= 0,
                Value = "5% of Coverage A",
                HurRated = false,
                LimitRated = false,
                NhrRated = false,
                Options = new List<RateOption>
                {
                    new RateOption()
                    {
                        Id = Guid.NewGuid(),
                        Value = "5% of Coverage A",
                        LimitFactor = 0.05,
                        LimitFlatAmount = 0,
                        HurFactor = .10,
                        HurFlatAmount = 0,
                        NhrFactor = .15,
                        NhrFlatAmount = 0,
                        LimitBasedOff = new List<Guid>{coverageA.Id},
                        HurBasedOff = new List<Guid>{coverageA.Id},
                        NhrBasedOff = new List<Guid>{coverageA.Id},
                        Selected = true
                    },
                    new RateOption()
                    {
                        Id = Guid.NewGuid(),
                        Value = "10% of Coverage A",
                        LimitFactor = 0.10,
                        LimitFlatAmount = 0,
                        HurFactor = .12,
                        HurFlatAmount = 0,
                        NhrFactor = .22,
                        NhrFlatAmount = 0,
                        LimitBasedOff = new List<Guid>{coverageA.Id},
                        HurBasedOff = new List<Guid>{coverageA.Id},
                        NhrBasedOff = new List<Guid>{coverageA.Id},
                        Selected = false
                    }
                }
            };
        }

        [TestMethod]
        public void SaveTest()
        {
            //assign
            var rateBook = BuildRateBook("HO3", "FL");
            rateBook.Pages = new List<RatePage> { BuildRatePage("Coverage Selection", 1) };
            rateBook.Pages[0].Sections = new List<RateSection> { BuildRateSection("Standard Coverages", 1) };
            rateBook.Pages[0].Sections[0].Items = new List<RateItem> { GetCoverageA() };
            rateBook.Pages[0].Sections[0].Items.Add(GetCoverageB(rateBook.Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A"))));
            //act
            IMongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("MyDB");
            var objectList1Collection = db.GetCollection<RateBook>("ObjectList1");
            objectList1Collection.InsertOne(rateBook);
            //assert
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void LoadTest()
        {
            //assign
            var rateBook = BuildRateBook("HO3", "FL");
            rateBook.Pages = new List<RatePage> { BuildRatePage("Coverage Selection", 1) };
            rateBook.Pages[0].Sections = new List<RateSection> { BuildRateSection("Standard Coverages", 1) };
            rateBook.Pages[0].Sections[0].Items = new List<RateItem> { GetCoverageA() };
            rateBook.Pages[0].Sections[0].Items.Add(GetCoverageB(rateBook.Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A"))));
            //act
            IMongoClient client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("MyDB");
            var objectList1Collection = db.GetCollection<RateBook>("ObjectList1");
            var expect = objectList1Collection.FindSync(book => book.PlanId.Equals("HO3")).FirstOrDefault();
            //assert
            Assert.AreEqual(rateBook, expect);

        }

        [TestMethod]
        public void GetTest()
        {
            //assign
            var rateBook = BuildRateBook("HO3", "FL");
            rateBook.Pages = new List<RatePage> { BuildRatePage("Coverage Selection", 1) };
            rateBook.Pages[0].Sections = new List<RateSection> { BuildRateSection("Standard Coverages", 1) };
            rateBook.Pages[0].Sections[0].Items = new List<RateItem> {GetCoverageA()};
            rateBook.Pages[0].Sections[0].Items.Add(GetCoverageB(rateBook.Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A"))));
            var expected = BuildRateBook("HO3", "FL");
            expected.Pages = new List<RatePage> { BuildRatePage("Coverage Selection", 1) };
            expected.Pages[0].Sections = new List<RateSection> { BuildRateSection("Standard Coverages", 1) };
            expected.Pages[0].Sections[0].Items = new List<RateItem>();
            var covA = GetCoverageA();
            covA.Limit = 100000;
            covA.Nhr = 250;
            covA.Hur = 200;
            expected.Pages[0].Sections[0].Items.Add(covA);
            var covB = GetCoverageB(rateBook.Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A")));
            covB.Limit = 5000;
            covB.Nhr = 37.5;
            covB.Hur = 20;
            expected.Pages[0].Sections[0].Items.Add(covB);
            //act
            var rates = Rate.RatePolicy(rateBook);
            //assert
            Assert.AreEqual(expected, rates);
        }
    }
}
