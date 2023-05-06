using CakeShop.Models;

namespace CakeShop.ModelViews
{
    public class HomeViewVM
    {
        public List<TblTinTuc> TinTucs { get; set; }
        public List<ProductHomeVM> Products { get; set; }
        
    }
}
