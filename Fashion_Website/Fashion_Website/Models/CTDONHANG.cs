//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fashion_Website.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTDONHANG
    {
        public string MACTDH { get; set; }
        public int SoLuongDat { get; set; }
        public decimal DonGia { get; set; }
        public string TenSP { get; set; }
        public string KichCo { get; set; }
        public string MaDH { get; set; }
        public string MaSP { get; set; }
    
        public virtual DONHANG DONHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
