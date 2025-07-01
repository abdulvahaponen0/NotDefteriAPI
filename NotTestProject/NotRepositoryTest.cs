using DataAccess;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTestProject
{
    public class NotRepositoryTest
    {
        public DbContextOptions<NotDbContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<NotDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }
        [Fact]
        public async Task Ekleme_Testi()
        {
            //Arrange
            var option = CreateInMemoryOptions();
            var context= new NotDbContext(option);
            INotRepository repository = new NotRepository(context);
            var not = new NotModel { Baslik="ilk not",Tarih=new DateTime(2025,06,22),NotDegeri="ders çalışmak"};
            //act
            await repository.NotEkle(not);
            //Test
            var notTest=await context.notModels.FirstOrDefaultAsync();
            Assert.NotNull(notTest);
            Assert.Equal("ilk not", notTest.Baslik);
            //Assert.Equal("2025,06,22",notTest.Tarih.ToString());        
        }
        [Fact]
        public async Task Notlari_Listelenmeli_Testi()
        {
            //arrange
            var option = CreateInMemoryOptions();
            var context=new NotDbContext(option);
            context.notModels.Add(
                new NotModel { Id = 1, Baslik = "Test başlık", Tarih = new DateTime(2025 - 03 - 22), NotDegeri = "Test not" });
            context.notModels.Add(
                            new NotModel { Id = 2, Baslik = "Test başlık2", Tarih = new DateTime(2025 - 03 - 22), NotDegeri = "Test not" });   
            
            await context.SaveChangesAsync();
            var repo=new NotRepository(context);
            //act
            var result = await repo.NotlariListele();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2,result.Count());
            Assert.Contains(result, n => n.Baslik == "Test başlık2");
        }
        [Fact]
        public async Task Not_Guncelle_Test()
        {
            var option = CreateInMemoryOptions();
            var context= new NotDbContext(option);
            var not = new NotModel { Id = 1, Baslik="Başlık1",Tarih= new DateTime(2025 - 03 - 22) ,NotDegeri="Test not"};
            context.notModels.Add(not);
            await context.SaveChangesAsync();
            INotRepository notRepository=new NotRepository(context);
            var notGüncel = new NotModel { Id = 1, Baslik = "Başlık1 güncel", Tarih = new DateTime(2025 - 03 - 22), NotDegeri = "Test not güncel" };
            var (Success,mesaj)= await notRepository.NotGuncelle(1,notGüncel);
            Assert.NotNull(mesaj);
            // Assert.Equal(Success,true);
            Assert.Equal("Not başarıyla güncellendi", mesaj);
            Assert.True(Success);
            var guncellenmisNot =await context.notModels.FindAsync(1);
            Assert.Equal("Başlık1 güncel", guncellenmisNot.Baslik);
        }
    }
}
