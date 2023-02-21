using EndProject.DAL;
using EndProject.Models;

namespace EndProject.Areas.Manage.Services
{
    public class AdminLayoutServices
    {
        private readonly AppDbContext _context;

        public AdminLayoutServices(AppDbContext context)
        {
            this._context = context;
        }


        public List<ContactUs> GetContacts()
        {
            return _context.ContactUs.Where(x=>x.IsSeen == false).OrderByDescending(x=>x.Id).ToList();
        }


    }
}
