using System;

namespace ZFC.Shop.Entity
{
    [Table("CustomerPassword")]
    public class CustomerPassword
    {
        /// <summary>
        ///Id
        /// </summary>
        [Column(true)]
        public int Id { get; set; }
        /// <summary>
        ///CustomerId
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        ///Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///PasswordFormatId
        /// </summary>
        public int PasswordFormatId { get; set; }
        /// <summary>
        ///PasswordSalt
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        ///CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
