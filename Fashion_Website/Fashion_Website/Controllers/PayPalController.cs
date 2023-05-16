using Fashion_Website.Config;
using Fashion_Website.Models;
using Fashion_Website.Models.shoppingCart;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Fashion_Website.Controllers
{
    public class PayPalController : Controller
    {
        fashionDBEntities db = new fashionDBEntities();
        //GET: PayPal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //return RedirectToAction("Message", "Cart", new { mess = "Lỗi" });
                        return RedirectToAction("TrangChu", "Home");

                    }
                }
            }
            catch (Exception ex)
            {
                //return RedirectToAction("Message", "Cart", new
                //{
                //    mess = ex.Message
                //});
                return RedirectToAction("TrangChu", "Home");
            }
            ////lấy ra đơn đặt vé từ session
            //Cart gioHang = Session["gioHang"] as Cart;

            ////lấy ra hóa đơn đã tạo tạm thời từ session
            //HOADON hoaDonTemp = Session["hoaDonTemp"] as HOADON;



            ////thực hiện thêm mới hóa đơn vào database
            //mapHoaDon map = new mapHoaDon();
            //map.ThemMoiHD(hoaDonTemp);

            ////tìm chuyến bay từ database
            //ChuyenBay modelChuyenBay = db.ChuyenBays.SingleOrDefault(c => c.MaChuyenBay == donDatVe.MaChuyenBay);

            //// thực hiện trừ chỗ trống khi hoàn thành thêm hóa đơn
            //modelChuyenBay.SLGheTrong = modelChuyenBay.SLGheTrong - donDatVe.SoLuongVe;

            //// lưu 
            //db.SaveChanges();

            ////gửi mail thông báo thanh toán thành công 
            //new cnpmNC.Models.mapContactEmail.mapContactEmail().SendEmail(donDatVe.TaiKhoanDat, "Thanh toán thành công", "<p style=\"font-size:20px\">Cảm ơn bạn đã mua vé máy bay của chúng tôi <br/>Mã đơn vé của bạn là: " + donDatVe.MaDatVe);


            ////xóa đơn và hóa đơn trong session sau khi hoàn thành 
            //Session.Remove("donDatVe");
            //Session.Remove("hoaDonTemp");

            fashionDBEntities db = new fashionDBEntities();

            DONHANG donHang = Session["DonHang"] as DONHANG;
            CTDONHANG ctDonHang = Session["CTDH"] as CTDONHANG;

            string maKH = Session["IDKH"].ToString().Trim();

            HOADON modelHoaDon = new HOADON();
            modelHoaDon.MaHD = new Fashion_Website.Models.taoMa.taoMaHoaDon().TaoMaHoaDon();
            modelHoaDon.NgayLap = DateTime.Now;
            modelHoaDon.TongTien = donHang.TongTien;
            modelHoaDon.MaKH = maKH;
            modelHoaDon.MaAD = "AD001";
            modelHoaDon.MaDH = donHang.MaDH;
            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu
                db.HOADONs.Add(modelHoaDon);
                db.SaveChanges();
                Session["HoaDon"] = modelHoaDon;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }

            CTHOADON ctHoaDon = new CTHOADON();
            ctHoaDon.MACTHD = new Fashion_Website.Models.taoMa.taoMaCTHD().TaoMaCTHD();
            ctHoaDon.TenSP = ctDonHang.TenSP;
            ctHoaDon.DonGia = ctDonHang.DonGia;
            ctHoaDon.SoLuong = ctDonHang.SoLuongDat;
            ctHoaDon.KichCo = ctDonHang.KichCo;
            ctHoaDon.ThanhTien = ctDonHang.DonGia * ctDonHang.SoLuongDat;
            ctHoaDon.MaHD = modelHoaDon.MaHD;
            ctHoaDon.MaSP = ctDonHang.MaSP;
            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu
                db.CTHOADONs.Add(ctHoaDon);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }

    

            DONHANG dONHANG = Session["DonHang"] as DONHANG;

            var khachHang = db.KHACHHANGs.SingleOrDefault(k => k.MaKH == dONHANG.MaKH);

            new Fashion_Website.Models.mapContactEmail.mapContactEmail().SendEmail(khachHang.Email, "Thanh toán thành công", "<p style=\"font-size:20px\">Cảm ơn bạn đã đặt sản phẩm của chúng tôi <br/>Mã đơn hàng của bạn là: " + dONHANG.MaDH);

            Session.Remove("DonHang");
            Session.Remove("CTDH");
            Session.Remove("HoaDon");

            return RedirectToAction("SanPham", "Home");

        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }


        private Payment CreatePayment(APIContext apicontext, string redirectURl)
        {
            Item item = new Item();

            if (Session["DonHang"] != null)
            {

                // Tỷ giá hối đoái USD/VND hiện tại
                decimal usdToVndRate = 23465m;


                var d = GetCurrencyExchange("VND", "USD");

                DONHANG donhang = Session["DonHang"] as DONHANG;


                //decimal ThanhTien = 0;
                //string strThanhTien = "";

                ////tạo biến lưu mã ưu đãi đã chọn
                //if (String.IsNullOrEmpty(hoaDonTemp.MaUuDai) == true)
                //{
                //    hoaDonTemp.MaUuDai = "UD0001";
                //}

                //if (hoaDonTemp.MaUuDai == "UD0001")
                //{
                //    ThanhTien = (modelChuyenBay.GiaVe * donDatVe.SoLuongVe) / usdToVndRate;
                //    strThanhTien = Math.Round(ThanhTien, 0).ToString();
                //}
                //else
                //{
                //    // lấy ra thông tin Uu đãi chọn
                //    cnpmNC.Models.UuDai ThongTinUuDai = new cnpmNC.Models.mapUuDai.mapUuDai().LayThongTinUuDai(hoaDonTemp.MaUuDai);

                //    //lấy ra mức ưu đãi

                //    decimal MucUD = Convert.ToDecimal(ThongTinUuDai.MucUD);

                //    ThanhTien = (modelChuyenBay.GiaVe * donDatVe.SoLuongVe - (modelChuyenBay.GiaVe * donDatVe.SoLuongVe * (MucUD / 100))) / usdToVndRate;
                //    strThanhTien = Math.Round(ThanhTien, 0).ToString();
                //}

                decimal priceOneItem = (donhang.TongTien) / usdToVndRate;
                decimal p = Math.Round(priceOneItem * d, 0);

                int soLuong = 0;
                string strSoLuong = "";

                string strThanhTien = donhang.TongTien.ToString();

                List<CTDONHANG> ctdon = Session["CTDH"] as List<CTDONHANG>;
                if (ctdon != null)
                {
                    foreach (var ct in ctdon)
                    {
                            soLuong = soLuong + ct.SoLuongDat;
                            strSoLuong = soLuong.ToString();
                    }
                }
                else
                {
                    // Handle the case where the Session["CTDH"] is null
                }



                item = new Item()
                {
                    name = "Airplane Ticket " + "(" + donhang.TongTien + ")",
                    currency = "USD",
                    price = p.ToString(),
                    quantity = strSoLuong,
                    sku = donhang.MaDH,

                };


                var payer = new Payer()
                {
                    payment_method = "paypal"
                };

                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectURl + "&Cancel=true",
                    return_url = redirectURl
                };

                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = strThanhTien,
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = details.subtotal,
                    details = details,
                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    //item_list = itemList

                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrl
                };
            }

            return this.payment.Create(apicontext);
        }


        public Decimal GetCurrencyExchange(String localCurrency, String foreignCurrency)
        {
            var code = $"{localCurrency}_{foreignCurrency}";
            var newRate = FetchSerializedData(code);
            return newRate;
        }

        private Decimal FetchSerializedData(String code)
        {
            var url = "https://free.currconv.com/api/v7/convert?q=" + code + "&compact=y&apiKey=7cf3529b5d3edf9fa798";
            var webClient = new WebClient();
            var jsonData = String.Empty;

            var conversionRate = 1.0m;
            try
            {
                jsonData = webClient.DownloadString(url);
                var jsonObject = new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, decimal>>>(jsonData);
                var result = jsonObject[code];
                conversionRate = result["val"];

            }
            catch (Exception) { }

            return conversionRate;
        }
    }
}