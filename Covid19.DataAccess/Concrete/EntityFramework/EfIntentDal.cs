using Covid19.Core.DataAccess.EntityFramework;
using Covid19.DataAccess.Abstract;
using Covid19.Entities.ComplexTypes;
using Covid19.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Covid19.DataAccess.Concrete.EntityFramework
{
    public class EfIntentDal : EfEntityRepositoryBase<Intent, Covid19Context>, IIntentDal
    {
        public IntentMessage GetIntentMessage(int id)
        {
            using (Covid19Context context = new Covid19Context())
            {
                var result = from x1 in context.Intent
                             join x2 in context.MessageSend on x1.IntentId equals x2.ReferenceId
                             where x1.IntentId == id && x2.ReferenceType == 1
                             select new IntentMessage
                             {
                                 Text = x2.Text
                             };

                return result.FirstOrDefault();
            }
        }

        public List<IntentParameter> GetIntentParameters(int id)
        {
            using (Covid19Context context = new Covid19Context())
            {
                var result = from x1 in context.Intent
                             join x2 in context.Parameter on x1.IntentId equals x2.IntentId
                             where x1.IntentId == id
                             select new IntentParameter
                             {
                                 ParameterName = x2.ParameterName
                             };

                return result.ToList();
            }
        }
    }
}
