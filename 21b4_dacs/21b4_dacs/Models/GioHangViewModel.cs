namespace WebApplication1.Models
{
    public class GioHangViewModel
    {
        public IEnumerable<GioHang> DsGioHang { get; set; }
        public double TotalPrice { get; set; }
		public HoaDon HoaDon { get; set; }
	}
}
