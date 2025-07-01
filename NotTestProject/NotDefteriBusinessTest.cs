using Business;
using DataAccess;
using Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTestProject
{
    public class NotDefteriBusinessTest
    {
        [Fact]
        public async Task NotEkle_BasariliOlunca_SuccessTrueDonmeli()
        {
            //arrange
            var notModel = new NotModel { Id = 1, Baslik = "Test", NotDegeri = "Test içerik" };
            var mockRepo=new Mock<INotRepository>();
            mockRepo.Setup(repo => repo.NotEkle(notModel)).ReturnsAsync((true, "Not başarıyla eklendi."));
            var business=new NotDefteriBusiness(mockRepo.Object);
            //act
            var result=await business.NotEkle(notModel);
            //assert
            Assert.True(result.Success);
            Assert.Equal("Not başarıyla eklendi.",result.mesaj);
        }
        [Fact]
        public async Task NotListele_Business_Test()
        {
            //Arrgange
            var mockRepository = new Mock<INotRepository>();
            var sampleData = new List<NotModel>
            {
                new NotModel { Id = 1, Baslik = "Başlık1", NotDegeri = "Not değeri" },
                new NotModel { Id = 2, Baslik = "Başlık2", NotDegeri = "Not değeri2" }
            };
            mockRepository.Setup(repo => repo.NotlariListele()).ReturnsAsync(sampleData);
            var business=new NotDefteriBusiness(mockRepository.Object);
            //Act
            var result = await business.NotlariListele();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Başlık1", result[0].Baslik);
        }
        [Fact]
        public async Task NotGuncell_Business_Test()
        {
            //Arrange
            //var not = new NotModel { Id=1,Baslik="Test başlık",Tarih=DateTime.Now,NotDegeri="Test notu"};
            var notGuncellenecek = new NotModel { Baslik="Test başlık güncel",Tarih=DateTime.Now,NotDegeri="Test notu güncel"};
            var mock=new Mock<INotRepository>();
            mock.Setup(repo => repo.NotGuncelle(1, notGuncellenecek)).ReturnsAsync((true, "Not başarıyla güncellendi"));
            INotDefteriBusiness notDefteriBusiness = new NotDefteriBusiness(mock.Object);
            //Act
            var result=await notDefteriBusiness.NotGuncelle(1,notGuncellenecek);
            Assert.Equal("Not başarıyla güncellendi", result.mesaj);
            Assert.True(result.Success);
            Assert.NotNull(result.mesaj);
        }
    }
}
