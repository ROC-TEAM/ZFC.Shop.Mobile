using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    [Serializable]
    [Table("T_User")]
    public class User
    {
        /// <summary>
        ///UserID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        ///CN_Name
        /// </summary>
        public string CN_Name { get; set; }
        /// <summary>
        ///EN_Name
        /// </summary>
        public string EN_Name { get; set; }
        /// <summary>
        ///AD
        /// </summary>
        public string AD { get; set; }
        /// <summary>
        ///Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///Mobile
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        ///WWID
        /// </summary>
        public string WWID { get; set; }
        /// <summary>
        ///DepartmentID
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        ///ManagerWWID
        /// </summary>
        public string ManagerWWID { get; set; }
        /// <summary>
        ///VirtualManagerWWID
        /// </summary>
        public string VirtualManagerWWID { get; set; }
        /// <summary>
        ///K2_ActionerID
        /// </summary>
        public int K2_ActionerID { get; set; }
        /// <summary>
        ///K2_ActionerName
        /// </summary>
        public string K2_ActionerName { get; set; }
        /// <summary>
        ///Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        ///NeedUpdate
        /// </summary>
        public bool NeedUpdate { get; set; }
        /// <summary>
        ///NeedUpdateDate
        /// </summary>
        public DateTime NeedUpdateDate { get; set; }
        /// <summary>
        ///CreateBy
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        ///CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        ///LastUpdateBy
        /// </summary>
        public string LastUpdateBy { get; set; }
        /// <summary>
        ///LastUpdateDate
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
        /// <summary>
        ///Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///Department
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        ///Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        ///DefaultLanguage
        /// </summary>
        public string DefaultLanguage { get; set; }
        /// <summary>
        ///DefaultBand
        /// </summary>
        public string DefaultBand { get; set; }
        /// <summary>
        ///NeedUpdateBand
        /// </summary>
        public string NeedUpdateBand { get; set; }
        /// <summary>
        ///LegalEntity
        /// </summary>
        public string LegalEntity { get; set; }

        //[Column(ignore: true)]
        //public string ManagerCNName { get; set; }

        //[Column(ignore: true)]
        //public string ManagerENName { get; set; }

        //[Column(ignore: true)]
        //public string ManagerAD { get; set; }

        public string GetDisplayName()
        {
            string enname = this.EN_Name;
            string cnname = this.CN_Name;

            string result = string.Empty;
            if (string.IsNullOrEmpty(enname)) return cnname;
            if (string.IsNullOrEmpty(cnname)) return enname;

            return string.Format("{0} / {1}", cnname, enname);
        }

        public string GetPhone()
        {
            if (string.IsNullOrEmpty(this.Mobile)) return this.Phone;
            return this.Mobile;
        }
    }
}
