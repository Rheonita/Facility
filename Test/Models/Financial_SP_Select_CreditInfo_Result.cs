//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.Models
{
    using System;
    
    public partial class Financial_SP_Select_CreditInfo_Result
    {
        public int ID_Credit { get; set; }
        public string Credit_Description { get; set; }
        public double Sum_of_Credit { get; set; }
        public System.DateTime Date_of_issue { get; set; }
        public short Credit_Term { get; set; }
        public byte Year_Percent { get; set; }
        public double Fine_Sum { get; set; }
        public Nullable<double> Sum_of_Month_Pay { get; set; }
        public Nullable<double> Total_Sum_With_Year { get; set; }
    }
}