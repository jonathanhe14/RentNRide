using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class DocumentoManager
    {

        private DocumentoCrudFactory crudDocumento;

        public DocumentoManager()
        {
            crudDocumento = new DocumentoCrudFactory();
        }

        public void Create(Documento documento)
        {
            try
            {
                var c = crudDocumento.Retrieve<Documento>(documento);

                if (c != null)
                {
                    //Customer already exist
                    throw new BussinessException(45);
                }

                    crudDocumento.Create(documento);
               
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Documento> RetrieveAll()
        {
            return crudDocumento.RetrieveAll<Documento>();
        }

        public Documento RetrieveById(Documento documento)
        {
            Documento c = null;
            try
            {
                c = crudDocumento.Retrieve<Documento>(documento);
                if (c == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(Documento documento)
        {
            crudDocumento.Update(documento);
        }

        public void Delete(Documento documento)
        {
            crudDocumento.Delete(documento);
        }

    }
}
