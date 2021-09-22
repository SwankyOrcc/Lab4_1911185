using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_1911185
{
	public class QuanlySV
	{
		public delegate int Sosanh(object sv1, object sv2);
		public List<SinhVien> dsSinhVien;
		public QuanlySV()
		{
			dsSinhVien = new List<SinhVien>();
		}
		public void Them(SinhVien sv)
		{
			this.dsSinhVien.Add(sv);
		}
		public SinhVien this[int index]
		{
			get { return dsSinhVien[index]; }
			set { dsSinhVien[index] = value; }
		}

		public void Xoa(object obj,Sosanh ss)
		{
			int i = dsSinhVien.Count - 1;
			for (; i >= 0; i--)
				if (ss(obj, this[i]) == 0)
					this.dsSinhVien.RemoveAt(i);
		}

		public SinhVien Tim(object obj,Sosanh ss)
		{
			SinhVien svresult = null;
			foreach(SinhVien sv in dsSinhVien)
				if(ss(obj,sv)==0)
				{
					svresult = sv;
					break;
				}
			return svresult;
		}

		public bool Sua(SinhVien svsua, object obj, Sosanh ss)
		{
			int i, count;
			bool kq = false;
			count = this.dsSinhVien.Count - 1;
			for (i = 0; i < count; i++)
				if (ss(obj, this[i]) == 0)
				{
					this[i] = svsua;
					kq = true;
					break;
				}
			return kq;
		}

		public void DocTuFile()
		{
			string filename = "DanhsachSV.txt", t;
			string[] s;
			SinhVien sv;
			StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open));
			while ((t = sr.ReadLine()) != null)
			{
				s = t.Split(',');
				sv = new SinhVien();
				sv.Maso = s[0];
				sv.Hoten = s[1];
				sv.Gioitinh = false;
				if (s[2] == "1")
					sv.Gioitinh = true;
				sv.Ngaysinh = DateTime.Parse(s[3]);
				sv.Lop = s[4];
				sv.SDT = s[5];
				sv.Email = s[6];
				sv.Diachi = s[7];
				sv.Hinh = s[8];
				this.Them(sv);

			}
		}
	}
}
