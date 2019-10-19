﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class PlaceOrderViewModelV2
    {
        public List<AvailInvViewModel> availInventory { get; set; }
        public List<int> custBought { get; set; }
        [Range(0, int.MaxValue)]
        public List<int> Quantity { get; set; }
        public int CustID { get; set; }
        public int LocID { get; set; }
    }
}