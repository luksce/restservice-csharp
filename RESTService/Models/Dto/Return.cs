using System;
using System.Collections.Generic;
using System.Text;

namespace RESTService.Models.Dto
{
    public class Return
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }

        public Return()
        {

        }
        public Return(string msg)
        {
            this.Codigo = 1;
            this.Mensagem = msg;
        }

        public Return(Exception exc)
        {
            this.Codigo = -1;
            this.Mensagem = "Erro: " + exc.Message;
        }

        public Return Erro() { Codigo = -1; return this; }
    }
}
