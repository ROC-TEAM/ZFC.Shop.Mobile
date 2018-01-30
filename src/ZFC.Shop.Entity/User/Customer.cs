using System;

namespace ZFC.Shop.Entity
{
    [Serializable]
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        ///// <summary>
        /////Id
        ///// </summary>
        //[Column(true)]
        //public int Id { get; set; }
        /// <summary>
        ///CustomerGuid
        /// </summary>
        public Guid CustomerGuid { get; set; }
        /// <summary>
        ///Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        ///Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///EmailToRevalidate
        /// </summary>
        public string EmailToRevalidate { get; set; }
        /// <summary>
        ///AdminComment
        /// </summary>
        public string AdminComment { get; set; }
        /// <summary>
        ///IsTaxExempt
        /// </summary>
        public bool IsTaxExempt { get; set; }
        /// <summary>
        ///AffiliateId
        /// </summary>
        public int AffiliateId { get; set; }
        /// <summary>
        ///VendorId
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        ///HasShoppingCartItems
        /// </summary>
        public bool HasShoppingCartItems { get; set; }
        /// <summary>
        ///RequireReLogin
        /// </summary>
        public bool RequireReLogin { get; set; }
        /// <summary>
        ///FailedLoginAttempts
        /// </summary>
        public int FailedLoginAttempts { get; set; }
        /// <summary>
        ///CannotLoginUntilDateUtc
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }
        /// <summary>
        ///Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        ///Deleted
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        ///IsSystemAccount
        /// </summary>
        public bool IsSystemAccount { get; set; }
        /// <summary>
        ///SystemName
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        ///LastIpAddress
        /// </summary>
        public string LastIpAddress { get; set; }
        /// <summary>
        ///CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        /// <summary>
        ///LastLoginDateUtc
        /// </summary>
        public DateTime LastLoginDateUtc { get; set; }
        /// <summary>
        ///LastActivityDateUtc
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }
        /// <summary>
        ///RegisteredInStoreId
        /// </summary>
        public int RegisteredInStoreId { get; set; }
        /// <summary>
        ///BillingAddress_Id
        /// </summary>
        public int BillingAddress_Id { get; set; }
        /// <summary>
        ///ShippingAddress_Id
        /// </summary>
        public int ShippingAddress_Id { get; set; }
    }
}
