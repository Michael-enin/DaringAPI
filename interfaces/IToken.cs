using System.Threading.Tasks;
using DaringAPI.Entities;

namespace DaringAPI.interfaces
{
    public interface IToken
    {
         Task<string> createToken(AppUser user);
    }
}