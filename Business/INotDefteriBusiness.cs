using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface INotDefteriBusiness
    {
        public Task<(bool Success, string mesaj)> NotEkle(NotModel notModel);
        public Task<List<NotModel>> NotlariListele();
        public Task<(bool Success,string mesaj)> NotGuncelle(int id, NotModel notModel);
        public Task Sil(int id);
    }
}
