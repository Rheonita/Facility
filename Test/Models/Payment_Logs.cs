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
    using System.Collections.Generic;
    
    public partial class Payment_Logs
    {
        public int ID_PaymentLog { get; set; }
        public int FK_Employer { get; set; }
        public Nullable<int> Amount_of_work { get; set; }
        public Nullable<double> Sum_of_Bonus { get; set; }
        public Nullable<double> Salary { get; set; }
        public Nullable<double> Total_Payment { get; set; }
        public Nullable<int> BuyStock_Amount { get; set; }
        public Nullable<int> Manufacture_Amount { get; set; }
        public Nullable<int> Sales_Amount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<double> Additional_Pay { get; set; }
    
        public virtual Employers Employers { get; set; }
    }
}
