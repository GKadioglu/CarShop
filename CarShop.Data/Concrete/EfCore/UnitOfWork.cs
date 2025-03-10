using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;

namespace CarShop.Data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        public UnitOfWork(ShopContext context)
        {
            _context = context;
        }
        private EfCoreCarRepository _carRepository;
        private EfCoreCategoryRepository _categoryRepository;
        private EfCoreModelRepository _modelRepository;
        private EfFavoriteCarRepository _favoriteCarRepository;
        private EfCoreAnonimMessageRepository _anonimMessageRepository;
        private EfCoreAdminMessageRepository _adminMessageRepository;
        private EfCoreAdminMakeAnOfferRepository _adminMakeAnOfferRepository;
        private EfCoreUserMakeAnOfferRepository _userMakeAnOfferRepository;
        private EfCoreModel3DRepository _model3DRepository;
        private EfCoreNotificationRepository _notificationRepository;


        public ICarRepository Cars =>
        _carRepository = _carRepository ?? new EfCoreCarRepository(_context);

        public ICategoryRepository Categories =>
        _categoryRepository = _categoryRepository ?? new EfCoreCategoryRepository(_context);

        public IModelRepository Models =>
        _modelRepository = _modelRepository ?? new EfCoreModelRepository(_context);

        public IFavoriteCarRepository FavoriteCars => _favoriteCarRepository = _favoriteCarRepository ?? new EfFavoriteCarRepository(_context);

        public IAnonimMessageRepository AnonimMessages => _anonimMessageRepository = _anonimMessageRepository ?? new EfCoreAnonimMessageRepository(_context);
        public IAdminMessageRepository AdminMessages =>
        _adminMessageRepository = _adminMessageRepository ?? new EfCoreAdminMessageRepository(_context);

        public IUserMakeAnOfferRepository UserMakeAnOffers => _userMakeAnOfferRepository = _userMakeAnOfferRepository ?? new EfCoreUserMakeAnOfferRepository(_context);
        public IAdminMakeAnOfferRepository AdminMakeAnOffers =>
        _adminMakeAnOfferRepository = _adminMakeAnOfferRepository ?? new EfCoreAdminMakeAnOfferRepository(_context);

        public IModel3DRepository Model3D =>
        _model3DRepository = _model3DRepository ?? new EfCoreModel3DRepository(_context);

        public INotificationRepository Notifications =>
        _notificationRepository = _notificationRepository ?? new EfCoreNotificationRepository(_context);


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}