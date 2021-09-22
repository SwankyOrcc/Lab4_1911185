using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_1911185
{
	public partial class FrmQuanlySV : Form
	{
		QuanlySV qlsv;
		public FrmQuanlySV()
		{
			InitializeComponent();
		}

		private void FrmQuanlySV_Load(object sender, EventArgs e)
		{
			qlsv = new QuanlySV();
			qlsv.DocTuFile();
			LoadListView();
		}

		private SinhVien GetSinhVien()
		{
			SinhVien sv = new SinhVien();
			bool gt = true;
			sv.Maso = this.mtbMSSV.Text;
			sv.Hoten = this.txtHovaTen.Text;
			if (rdbNu.Checked)
				gt = false;
			sv.Gioitinh = gt;
			sv.Ngaysinh = this.dtpNgaysinh.Value;
			sv.Lop = this.cbbLop.Text;
			sv.SDT = this.mtbSDT.Text;
			sv.Email = this.txtEmail.Text;
			sv.Diachi = this.txtDiaChi.Text;
			sv.Hinh = this.txtPicture.Text;
			return sv;
		}

		private SinhVien GetSinhVienLV(ListViewItem lvitem)
		{
			SinhVien sv = new SinhVien();
			sv.Maso = lvitem.SubItems[0].Text;
			sv.Hoten = lvitem.SubItems[1].Text;
			sv.Gioitinh = false;
			if (lvitem.SubItems[2].Text == "Nam")
				sv.Gioitinh = true;
			sv.Ngaysinh = DateTime.Parse(lvitem.SubItems[3].Text);
			sv.Lop = lvitem.SubItems[4].Text;
			sv.SDT = lvitem.SubItems[5].Text;
			sv.Email = lvitem.SubItems[6].Text;
			sv.Diachi = lvitem.SubItems[7].Text;
			sv.Hinh = lvitem.SubItems[8].Text;
			return sv;
		}

		private void ThietlapThongtin(SinhVien sv)
		{
			this.mtbMSSV.Text = sv.Maso;
			this.txtHovaTen.Text = sv.Hoten;
			if (sv.Gioitinh)
				this.rdbNam.Checked = true;
			else
				this.rdbNu.Checked = true;
			this.dtpNgaysinh.Value = sv.Ngaysinh;
			this.cbbLop.Text = sv.Lop;
			this.mtbSDT.Text = sv.SDT;
			this.txtEmail.Text = sv.Email;
			this.txtDiaChi.Text = sv.Diachi;
			this.txtPicture.Text = sv.Hinh;
		}

		private void ThemSV(SinhVien sv)
		{
			ListViewItem lvitem = new ListViewItem(sv.Maso);
			lvitem.SubItems.Add(sv.Hoten);
			string gt = "Nam";
			if (sv.Gioitinh)
				gt = "Nữ";
			lvitem.SubItems.Add(gt);
			lvitem.SubItems.Add(sv.Ngaysinh.ToShortDateString());
			lvitem.SubItems.Add(sv.Lop);
			lvitem.SubItems.Add(sv.SDT);
			lvitem.SubItems.Add(sv.Email);
			lvitem.SubItems.Add(sv.Diachi);
			lvitem.SubItems.Add(sv.Hinh);
			this.lvSinhVien.Items.Add(lvitem);
		}

		private void LoadListView()
		{
			this.lvSinhVien.Items.Clear();
			foreach(SinhVien sv in qlsv.dsSinhVien)
			{
				ThemSV(sv);
			}
		}

		private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
		{
			int count = this.lvSinhVien.SelectedItems.Count;
			if(count>0)
			{
				ListViewItem lvitem = this.lvSinhVien.SelectedItems[0];
				SinhVien sv = GetSinhVienLV(lvitem);
				ThietlapThongtin(sv);
			}
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnMacDinh_Click(object sender, EventArgs e)
		{
			this.mtbMSSV.Text = "";
			this.txtHovaTen.Text = "";
			this.rdbNam.Checked = true;
			this.dtpNgaysinh.Value = DateTime.Now;
			this.cbbLop.Text = this.cbbLop.Items[0].ToString();
			this.mtbSDT.Text = "";
			this.txtEmail.Text = "";
			this.txtDiaChi.Text = "";
			this.txtPicture.Text = "";
			this.pbSinhVien.ImageLocation = "";

		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			
			SinhVien sv = GetSinhVien();
			SinhVien kq = qlsv.Tim(sv.Maso, delegate (object obj1, object obj2)
			{
				return (obj2 as SinhVien).Maso.CompareTo(obj1.ToString());
			});
			bool kqsua;
			kqsua = qlsv.Sua(sv, sv.Maso, SoSanhTheoMa);
			if (kq != null)
			{
				if (kqsua)
				{
					this.LoadListView();
				}
				//MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu",
				//MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				this.qlsv.Them(sv);
				this.LoadListView();
			}
		}

		private int SoSanhTheoMa(object obj1, object obj2)
		{
			SinhVien sv = obj2 as SinhVien;
			return sv.Maso.CompareTo(obj1);
		}

		private void btnChonHinh_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Open Dialog";// "Add Photos";
			dlg.Multiselect = true;
			dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
			+ "*.jpg;*.jpeg;*.gif;*.bmp;"
			+ "*.tif;*.tiff;*.png|"
			+ "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
			+ "GIF files (*.gif)|*.gif|"
			+ "BMP files (*.bmp)|*.bmp|"
			+ "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
			+ "PNG files (*.png)|*.png|"
			+ "All files (*.*)|*.*";

			dlg.InitialDirectory = Environment.CurrentDirectory;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var filename = dlg.FileName;
				txtPicture.Text = filename;
				pbSinhVien.Load(filename);
			}
		}
	}
}
