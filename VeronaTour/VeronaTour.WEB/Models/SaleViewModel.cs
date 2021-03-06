﻿using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class SaleViewModel
    {
        public SaleSettingsDTO Sale { get; set; }
        public int SelectedMaxSale { get; set; }
        public int SelectedSaleStep { get; set; }
    }
}