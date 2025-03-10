using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars { get; }
        ICategoryRepository Categories { get; }
        IModelRepository Models { get; }
        IFavoriteCarRepository FavoriteCars { get; }
        IAnonimMessageRepository AnonimMessages { get; }
        IAdminMessageRepository AdminMessages { get; }
        IModel3DRepository Model3D { get; }
        INotificationRepository Notifications { get; }
        IUserMakeAnOfferRepository UserMakeAnOffers { get; }
        IAdminMakeAnOfferRepository AdminMakeAnOffers { get; }
        void Save();
        Task<int> SaveAsync();

    }
}