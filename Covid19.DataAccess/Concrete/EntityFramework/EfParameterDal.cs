using Covid19.Core.DataAccess.EntityFramework;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;
using System.Linq;

namespace Covid19.DataAccess.Concrete.EntityFramework
{
    public class EfParameterDal : EfEntityRepositoryBase<Parameter, Covid19Context>, IParameterDal
    {
        public ParameterMessage GetParameterMessage(int id)
        {
            using (Covid19Context context = new Covid19Context())
            {
                var result = from x1 in context.Parameter
                             join x2 in context.MessageSend on x1.ParameterId equals x2.ReferenceId
                             where x1.ParameterId == id && x2.ReferenceType == 2
                             select new ParameterMessage
                             {
                                 Text = x2.Text
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
