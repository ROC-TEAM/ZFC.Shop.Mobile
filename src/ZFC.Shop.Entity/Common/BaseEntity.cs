
namespace ZFC.Shop.Entity
{
    /// <summary>
    /// 公共实体基类
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        [Column(true)]
        public int Id { get; set; }
    }
}
