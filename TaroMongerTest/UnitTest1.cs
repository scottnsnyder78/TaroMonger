using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaroMongerCore;

namespace TaroMongerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTest()
        {
            //assign
            var x = new RateBook
            {
                EffectiveDateTime = DateTime.Now,
                Id = new Guid(),
                PlanId = "HO3",
                State = "FL",
                Pages = new List<RatePage> {
                    new RatePage
                    {
                        Name = "Coverage Selection",
                        Number = 1,
                        Sections = new List<RateSection>
                        {
                            new RateSection
                            {
                                Number = 1,
                                Name = "Standard Coverages",
                                Items = new List<RateItem>
                                {
                                    new RateItem()
                                    {
                                        Name = "Coverage A",
                                        Id = new Guid(),
                                        BasedOff = new List<Guid>(),
                                        Factor = 0,
                                        FlatAmount = 0,
                                        Value = string.Empty,
                                        Options = new List<RateOption>
                                        {
                                            new RateOption()
                                            {
                                                Value = "100000",
                                                FlatAmount = 500,
                                                Factor = 0
                                            },
                                            new RateOption()
                                            {
                                                Value = "101000",
                                                FlatAmount = 550,
                                                Factor = 0
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }

                    },
                    new RatePage
                    {
                        Name = "Underwriting",
                        Number = 2,
                        Sections = new List<RateSection>()
                    }
                }
            };
            //act
            //assert
        }
    }
}
