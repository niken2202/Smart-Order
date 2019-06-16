namespace Data.Infrastructure
{
    public class DbFactory :Disposable,IDbFactory
    {
        private SmartOrderContext dbContext;
        public SmartOrderContext Init()
        {
            return dbContext ?? (dbContext = new SmartOrderContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}