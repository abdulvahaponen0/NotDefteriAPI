using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class NotDefteriBusiness : INotDefteriBusiness
    {
        private readonly INotRepository _repository;
        public NotDefteriBusiness(INotRepository repository)
        {
            _repository = repository;
        }
        public async Task<(bool Success, string mesaj)> NotEkle(NotModel notModel)
        {
            return await _repository.NotEkle(notModel);
        }

        public async Task<(bool Success,string mesaj)> NotGuncelle(int id,NotModel notModel)
        {
            return await _repository.NotGuncelle(id,notModel);
        }

        public async Task<List<NotModel>> NotlariListele()
        {
            return await _repository.NotlariListele();
            //throw new NotImplementedException();
        }

        public async Task Sil(int id)
        {
             await _repository.Sil(id);
        }
    }
}
