using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    //aşağıdaki yorum satırında oldugu gibi OUT SAYESİNDE Category'yi istersem entity halinde istersem IList olarak gönderebildim. 
    public interface IDataResult<out T>:IResult
    {
        public T Data { get;} // new DataResult<Category>(ResultStatus.Success,category);
                              // new DataResult<IList<Category>>(ResultStatus.Success, categoryList);
    }
}
