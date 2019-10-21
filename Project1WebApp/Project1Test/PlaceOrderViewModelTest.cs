using Project1WebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Project1Test
{
    public class PlaceOrderViewModelTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-4)]

        public void QuantityShouldAlwaysBePositive(int i)
        {
            try
            {
                new PlaceOrderViewModelV2()
                {
                    Quantity = new List<int>()
                    {
                        i                       
                    }
                };
                Assert.True(false);
            }
            catch 
            {
                Assert.True(true);
            }
        }
    }
}
