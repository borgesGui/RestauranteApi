using RestauranteApi.Context;

namespace RestauranteApi.Bll
{
    public abstract class AbstractBll
    {
        protected readonly RestauranteDbContext _context;
        private bool _saveChanges;

        protected AbstractBll(RestauranteDbContext context)
        {
            _context = context;
            _saveChanges = true;
        }

        protected AbstractBll(RestauranteDbContext context, bool saveChanges)
        {
            _context = context;
            _saveChanges = saveChanges;
        }
        
        protected bool SaveChanges
        {
            get
            {
                return this._saveChanges;
            }
            set
            {
                this._saveChanges = value;
            }
        }

        protected void ExecuteSaveChanges()
        {
            if (_saveChanges)
            {
                _context.SaveChanges();
            }
        }
    }
}