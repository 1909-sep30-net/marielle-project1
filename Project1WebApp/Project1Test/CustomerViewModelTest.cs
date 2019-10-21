﻿using Project1WebApp.Models;
using Xunit;

namespace Project1Test
{
    public class CustomerViewModelTest
    {
        [Fact]
        public void CustomerShouldValidateStrings()
        {
            try
            {
                CustomerViewModel c = new CustomerViewModel()
                {
                    FirstName = "",
                    LastName = ""
                };
                Assert.True(false, "Failed");
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        public void CustomerShouldValidateStreets(string input)
        {
            try
            {
                new CustomerViewModel()
                {
                    Street = input
                };
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("12 Main")]
        [InlineData("1 Main")]
        [InlineData("       3 Main         ")]
        public void CustomerShouldAddValidStreets(string input)
        {
            try
            {
                new CustomerViewModel()
                {
                    Street = input
                };
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        public void CustomerShouldValidateCity(string input)
        {
            try
            {
                new CustomerViewModel()
                {
                    City = input
                };
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("Main")]
        [InlineData("Main City")]
        [InlineData("      Metropolis        ")]
        public void CustomerShouldAddValidCities(string input)
        {
            try
            {
                new CustomerViewModel()
                {
                    City = input
                };
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Theory]
        [InlineData(34)]
        [InlineData(1)]
        [InlineData(123)]
        public void CustomerShouldValidateZipcode(int input)
        {
            try
            {
                new CustomerViewModel()
                {
                    Zipcode = input
                };
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData(12345)]
        [InlineData(94578)]
        [InlineData(94577)]
        public void CustomerShouldAddValidZipcodes(int input)
        {
            try
            {
                new CustomerViewModel()
                {
                    Zipcode = input
                };
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}