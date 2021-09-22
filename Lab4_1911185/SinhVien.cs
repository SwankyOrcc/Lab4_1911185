using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_1911185
{
	public class SinhVien
	{
		public string Maso { get; set; }
		public string Hoten { get; set; }
		public bool Gioitinh { get; set; }
		public DateTime Ngaysinh { get; set; }
		public string Lop { get; set; }
		public string SDT { get; set; }
		public string Email { get; set; }
		public string Diachi { get; set; }
		public string Hinh { get; set; }
		public SinhVien()
		{

		}

		public SinhVien(string ms,string hoten,bool gioitinh,DateTime ngaysinh,string lop,string sdt,string email,string diachi,string hinh)
		{
			this.Maso = ms;
			this.Hoten = hoten;
			this.Gioitinh = gioitinh;
			this.Ngaysinh = ngaysinh;
			this.Lop = lop;
			this.SDT = sdt;
			this.Email = email;
			this.Diachi = diachi;
			this.Hinh = hinh;
		}
	}
}
