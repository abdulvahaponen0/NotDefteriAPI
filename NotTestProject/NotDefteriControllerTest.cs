using Business;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotDefteriAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTestProject
{
    public class NotDefteriControllerTest
    {
        [Fact]
        public async Task NotEkle_GecerliModel_BasariliEklemeDonmeli()
        {
            //arrange
            var notModel = new NotModel {Id=1,Baslik="Başlık",NotDegeri="İçerik" };
            var mockBusiness = new Mock<INotDefteriBusiness>();
            mockBusiness.Setup(b => b.NotEkle(notModel)).ReturnsAsync((true, "Not başarıyla eklendi."));
            var controller=new NotDefteriController(mockBusiness.Object);

            //Act
            var result=await controller.NotEkle(notModel);

            //assert
            var okResult=Assert.IsType<OkObjectResult>(result);
            var returnedModel=Assert.IsType<NotModel>(okResult.Value);
            Assert.Equal(notModel.Baslik,returnedModel.Baslik);
        }
        [Fact]
        public async Task Not_Guncell_Controller()
        {
            //Arrange
            var notGuncellenecek = new NotModel { Baslik = "Test başlık güncel", Tarih = DateTime.Now, NotDegeri = "test notu güncel" };
            var mockBusiness = new Mock<INotDefteriBusiness>();
            mockBusiness.Setup(repo => repo.NotGuncelle(1, notGuncellenecek)).ReturnsAsync((true,"Not başarılı bir şekilde güncellendi"));
            var controller=new NotDefteriController(mockBusiness.Object);
            //Act
            var result =await controller.NotGuncelle(1, notGuncellenecek);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // 200 OK döndü mü?
            var returnedModel = Assert.IsType<NotModel>(okResult.Value); // Model tipi doğru mu?

            Assert.Equal("Test başlık güncel", returnedModel.Baslik); // İçerik kontrolü
            Assert.Equal("test notu güncel", returnedModel.NotDegeri);
        }
    }
}
