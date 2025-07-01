using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NotRepository : INotRepository
    {
        private readonly NotDbContext _context;
        public NotRepository(NotDbContext context)
        {
                _context = context;
        }
        public async Task<(bool Success,string mesaj)> NotEkle(NotModel notModel)
        {
            try
            {
                await _context.notModels.AddAsync(notModel);
                await _context.SaveChangesAsync();
                return (true,"Not başarıyla eklendi.");
            }
            catch(DbUpdateException dbex) {
                return (false,dbex.Message);
            }
            catch (Exception ex)
            {
                return (false,ex.Message);
            }
        }
        //Güncelleme fonksiyonu
        public async Task<(bool Success,string mesaj)> NotGuncelle(int id, NotModel notModel)
        {
            var not=await _context.notModels.FindAsync(id);
            if (not == null)
            {
                return (false, $"ID {id} ile eşleşen not bulunamadı.");
            }
            try
            {
                not.Baslik = notModel.Baslik;
                not.Tarih = notModel.Tarih;
                not.NotDegeri = notModel.NotDegeri;
                await _context.SaveChangesAsync();
                return (true,"Not başarıyla güncellendi");
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                return (false, $"Veritabanı güncelleme hatası: {innerMessage}");
            }
            catch(Exception ex)
            {
                return (false, $"Bilinmeyen bir hata oluştu: {ex.Message}");
            }
        }
        //notları listele fonksiyonu
        public async Task<List<NotModel>> NotlariListele()
        {
            var notListesi=await _context.notModels.ToListAsync();
            if (notListesi == null) {
                throw new Exception("not bulunamadı!");
            }
            return notListesi;
        }

        public async Task Sil(int id)
        {
            try
            {
                var silinecekNot = await _context.notModels.FindAsync(id);
                if (silinecekNot == null)
                {
                    throw new Exception("not bulunamadı!");
                }
                _context.notModels.Remove(silinecekNot);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Hata!: "+ex.ToString());
            }
            
        }
    }
}
