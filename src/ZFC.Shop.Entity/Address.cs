using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    [Table(nameof(Address))]
    public class Address : BaseEntity
    {
        /// <summary>
        /// 省份 增加的
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        ///FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        ///LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        ///Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///Company
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        ///CountryId
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        ///StateProvinceId
        /// </summary>
        public int StateProvinceId { get; set; }
        /// <summary>
        ///City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        ///Address1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        ///Address2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        ///ZipPostalCode
        /// </summary>
        public string ZipPostalCode { get; set; }
        /// <summary>
        ///PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        ///FaxNumber
        /// </summary>
        public string FaxNumber { get; set; }
        /// <summary>
        ///CustomAttributes
        /// </summary>
        public string CustomAttributes { get; set; }
        /// <summary>
        ///CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
